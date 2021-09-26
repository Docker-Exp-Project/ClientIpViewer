# ClientIpViewer
Set up the docker with nginx as a reverse proxy for getting actual client ip address.

# Running project(dotnet)
1. Go to the directory where the project exist i.e. /ClientIpViewer/
2. Then run command "dotnet build" to build the project
3. If you want to run the project then use "dotnet run"
4. If you want to create publish files then run command "dotnet push -c release -o out"
5. Upper command will create folder called "out" which will contain your all push files

# Running Container(docker)
1. Go to the directory where the project exist i.e. /ClientIpViewer/
2. Then run command "docker build -t <Image_Name>"
3. For running docker, type command "docker run -p 8080:80 <Image_Name>"

# Running Services(docker-compose)
1. Go to the directory where the project exist i.e. /ClientIpViewer/
2. Then run command "docker-compose up --build"
3. Upper command will build your project and then start running the project
4. if you want your project to run on deattach mode then use "docker-compose up -d --build"


