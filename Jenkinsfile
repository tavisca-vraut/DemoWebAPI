pipeline 
{
    agent any
    parameters 
    {
        string(name: 'REPO_PATH', defaultValue: 'https://github.com/tavisca-vraut/DemoWebAPI.git')
        string(name: 'SOLUTION_PATH', defaultValue: 'DemoWebApp.sln')
        string(name: 'TEST_PATH', defaultValue: 'DemoTest/DemoTest.csproj')
        choice(name: 'JOB', choices:  ['Build' , 'Test'])
        string(name: 'NUGET_REPO', defaultValue: 'https://api.nuget.org/v3/index.json')
    }
    environment
    {
        projectToBePublished = 'DemoWebApp'
        restoreCommand = 'dotnet restore $env:SOLUTION_PATH --source $env:NUGET_REPO'
        buildCommand = 'dotnet build $env:SOLUTION_PATH -p:Configuration=release -v:n'
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
                powershell(script: "echo '***************Recovery Finish********************'")
            }
        }
        stage('Test') 
        {
            when
            {
                expression { params.JOB == 'Test' }
            }
            
            steps 
            {
                powershell(script: 'dotnet test $env:TEST_PATH')
            }
        }
        stage('Publish') 
        {
            steps 
            {
                powershell(script: 'dotnet publish $env:projectToBePublished -c Release -o artifacts')
            }
        }
    }
    post
    {
        success
        {
            zip zipfile: 'publish.zip', 'DemoWebApp/artifacts'
            archiveArtifacts artifacts: 'publish.zip'
        }
        always
        {
            deleteDir()
        }
    }
}
