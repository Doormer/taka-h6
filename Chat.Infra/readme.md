# Chat.Infra Project

## Overview

The `Chat.Infra` project is part of the Chat application infrastructure. It contains the Entity Framework Core
configurations, repositories, and the `ChatContext` class which is the database context for the application.

## Adding Migrations

To add migrations, use the following command inside the `Chat.Infra` project directory:

```sh
dotnet ef migrations add {migrationName} --startup-project ../Chat.ApiService
```

Replace `{migrationName}` with the name of your migration.

## Run Migration against database

To run migrations, use the following command inside the `Chat.Infra` project directory:

```sh
dotnet ef database update --startup-project ../Chat.ApiService --configuration Debug
```

## Project Structure

* Chat.Infra/ChatContext.cs: Contains the ChatContext class which is the database context.
* Chat.Infra/EntityConfigurations/: Contains entity type configurations.
* Chat.Infra/Repositories/: Contains repository implementations.

## Elasticsearch

### 1.pull image
```shell
docker pull elasticsearch:8.12.0
```
### 2.docker run 
```shell
docker run -d \
  --name elasticsearch \
  -p 9200:9200 \
  -p 9300:9300 \
  -e "discovery.type=single-node" \
  -e "xpack.security.enabled=false" \
  -e "xpack.security.http.ssl.enabled=false" \
  elasticsearch:8.12.0
```
### 3.create index
```shell
http://localhost:9200/message  (PUT) (Context-Type: application/json)
{
    "settings": {
        "number_of_shards": "1",
        "number_of_replicas": "1"
    },
    "mappings": {
        "properties": {
            "content": {
                "type": "text",
                "analyzer": "standard"
            },
            "senderId": {
                "type": "keyword"
            },
            "receivedId": {
                "type": "keyword"
            },
            "Id": {
                "type": "long"
            },
            "sentTime": {
                "type": "date"
            },
            "isRead": {
                "type": "boolean"
            }
        }
    }
}
```

## Logstash
### 1.download and install
```shell
https://www.elastic.co/downloads/past-releases/logstash-8-12-0
```

### 2.install mysql plugin
```shell
bin/logstash-plugin install logstash-integration-jdbc
```

### 3.download mysql-connector-java-8.0.29.jar
```shell
curl -o /opt/mysql-connector-java-8.0.29.jar https://repo1.maven.org/maven2/mysql/mysql-connector-java/8.0.29/mysql-connector-java-8.0.29.jar
```

### 4.create last_run_metadata_path
```shell
touch /opt/message.txt
```

### 5.mysql_to_es.conf
```shell

```
```config
input {
  jdbc {
    jdbc_driver_library => "/opt/mysql-connector-java-8.0.29.jar"
    jdbc_driver_class => "com.mysql.cj.jdbc.Driver"
    jdbc_connection_string => "jdbc:mysql://localhost:3306/ChatDb?useUnicode=true&characterEncoding=utf-8&useSSL=false"
    jdbc_user => "root"
    jdbc_password => "yenngyenng"
    jdbc_default_timezone => "Pacific/Auckland"
    schedule => "* * * * *"  # 每分钟同步一次，可根据需要调整
    statement => "SELECT Id, SenderId as senderId,receiverId as receiverId, Content as content, SentTime as sentTime, IsRead as isRead FROM messages WHERE DATE_FORMAT(sentTime, '%Y-%m-%d %H:%i:%s') > :sql_last_value"
	  lowercase_column_names => false
    use_column_value => true
    tracking_column => "sentTime"
    tracking_column_type => "timestamp"
    last_run_metadata_path => "/opt/message.txt"
  }
}

output {
  elasticsearch {
    hosts => ["http://localhost:9200"]
    index => "message"
    document_type => "_doc"
  }
  stdout { codec => rubydebug }
}
```

### 6.start logstash
```shell
bin/logstash -f config/mysql_to_es.conf
```
