# NacosDockerSample

This repo contain some samples about running in docker using nacos-sdk-csharp.


## Service Registration and Service Discovery

### 1. Clone project

```sh
git clone --depth 1 https://github.com/catcherwong/NacosDockerSample.git
cd NacosDockerSample
```

### 2. Modify the configuration

Modify the `appsettings.json` file of App1 and App2.

```JSON
{ 
  "nacos": {
    "ServerAddresses": [ "replace with your nacos server address" ],
    "DefaultTimeOut": 15000,
    "Namespace": "replace with your namespace",
    "ListenInterval": 1000,
    "ServiceName": "replace with your servicename"
  }
}
```

### 3. Build docker images


```sh
docker build -t app1:v1 -f .\docker\Dockerfile.App1 .
docker build -t app2:v1 -f .\docker\Dockerfile.App2 .
```

### 4. Run up samples

```
docker run --name=app1 -d -p 9512:80 app1:v1
docker run --name=app11 -d -p 9516:80 app1:v1
docker run --name=app2 -d -p 9513:80 app2:v1
```

### 5. Enjoy it

```sh
curl localhost:9512/api/values/test
["value1","value2","App2222222222222222222222222"]

curl localhost:9513/api/values/test
["value1","value2","App1"]

curl localhost:9516/api/values/test
["value1","value2","App2222222222222222222222222"]
```
