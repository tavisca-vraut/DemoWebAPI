/*
 Add your docker credentials to jenkins credentials manager. Don't forget to give it an ID.
 When, prompted for ID in parameters, enter it.
*/


// Globals
def CustomImage

pipeline 
{
    agent any
    parameters 
    {
        string(name: 'JOB_NAME', defaultValue: 'Demo-WebApi-Test', description: 'Name of the current job that is going to run the pipeline.')

        string(name: 'APPLICATION_NAME', defaultValue: 'DemoWebApp', description: 'Name of the project that you want to test/deploy/etc.')
        string(name: 'SOLUTION_PATH', defaultValue: 'DemoWebApp.sln')
        string(name: 'TEST_PATH', defaultValue: 'DemoTest/DemoTest.csproj', description: 'Relative Path of the .csproj file of test project')
        
        string(name: 'DOCKER_HUB_USERNAME', defaultValue: 'vighnesh153')
        string(name: 'DOCKER_HUB_CREDENTIALS_ID', defaultValue: 'docker-hub-credentials')
        string(name: 'DOCKER_IMAGE_NAME', defaultValue: 'demo-web-app-test', description: 'Name of the image to be created')
        string(name: 'DOCKER_IMAGE_TAG', defaultValue: 'latest', description: 'Release information')
        choice(name: 'JOB', choices:  ['Test' , 'Build', 'Create Image'])
    }
    environment
    {
        // DON'T EDIT UNLESS YOU KNOW WHAT YOU ARE DOING
        nugetRepository = 'https://api.nuget.org/v3/index.json'

        restoreCommand = "dotnet restore ${env.SOLUTION_PATH} --source ${env.nugetRepository}"
        buildCommand = "dotnet build ${env.SOLUTION_PATH} -p:Configuration=release -v:n --no-restore"

        artifactsDirectory = "MyArtifacts"
    }
    stages 
    {
        stage('Build') 
        {
            steps
            {    
                powershell(script: "echo '*********Starting Restore and Build***************'")
                powershell(script: '$env:restoreCommand')
                powershell(script: '$env:buildCommand')
                powershell(script: "echo '***************Done Restore and Build********************'")
            
                powershell(script: "echo '*********Starting Test***************'")
                powershell(script: "dotnet test ${env.TEST_PATH}")
                powershell(script: "echo '*********Done Testing***************'")
            
                powershell(script: "echo '********* Publishing... ***************'")
                powershell(script: "dotnet publish ${env.APPLICATION_NAME} -c Release -o ${env.artifactsDirectory} --no-restore")
                powershell(script: "echo '********* Published ***************'")
            
                powershell(script: "echo '********* Move dockerfile to the artifacts directory for ease ***************'")
                powershell "mv Dockerfile ${env.APPLICATION_NAME}/${env.artifactsDirectory}"
                powershell(script: "echo '********* Move complete ***************'")
            
                powershell(script: "echo '********* Build docker image and push to DockerIO registry ***************'")
                script 
                {
                    dir("${env.APPLICATION_NAME}/${env.artifactsDirectory}") 
                    {
                        powershell "docker build -t ${env.DOCKER_HUB_USERNAME}/${env.DOCKER_IMAGE_NAME}:${env.DOCKER_IMAGE_TAG} --build-arg APPLICATION_NAME_TO_BE_HOSTED=${env.APPLICATION_NAME} ."

                        docker.withRegistry('https://www.docker.io/', "${env.DOCKER_HUB_CREDENTIALS_ID}") 
                        {
                            powershell "docker push ${env.DOCKER_HUB_USERNAME}/${env.DOCKER_IMAGE_NAME}:${env.DOCKER_IMAGE_TAG}"   
                        }
                    }
                }
                powershell(script: "echo '********* Docker immage build succeded and pushed to registry ***************'")
            }
        }
        stage('Deloy')
        {
            steps
            {
                powershell "docker run -p 7001:80 ${env.DOCKER_HUB_USERNAME}/${env.DOCKER_IMAGE_NAME}:${env.DOCKER_IMAGE_TAG}"
            }
        }
    }
    post
    {
        always
        {
            deleteDir()
        }
    }
}
