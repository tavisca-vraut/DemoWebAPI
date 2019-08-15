pipeline {
    agent any
    parameters {
        string(name: 'REPO_PATH', defaultValue: 'https://github.com/tavisca-vraut/DemoWebAPI.git')
        string(name: 'SOLUTION_PATH', defaultValue: 'DemoWebApp.sln')
        string(name: 'TEST_PATH', defaultValue: 'DemoTest/DemoTest.csproj')
        choice(name: 'JOB', choices:  ['Build' , 'Test'])
        string(name: 'NUGET_REPO', defaultValue: 'https://api.nuget.org/v3/index.json')
    }
    stages {
        stage ('Testing if the powershell commands work')
        {
            steps
            {
                powershell(script: 'echo "Hello1, $env:REPO_PATH"')
                powershell(script: 'echo "Hello3, $env:SOLUTION_PATH"')
            }
        }
        stage('Echo current directory')
        {
            steps
            {
                powershell(script: 'pwd')
            }
        }
        stage('Build') {
            steps {
                powershell(script: 'echo "*********Starting Restore and Build***************')
                powershell(script: 'dotnet restore $env:SOLUTION_PATH --source $env:NUGET_REPO')
                powershell(script: 'dotnet build $env:SOLUTION_PATH -p:Configuration=release -v:n')
                powershell(script: 'echo "***************Recovery Finish********************"')
            }
        }
        stage('Test') {
            when
            {
                expression { params.JOB == 'Test' }
            }
            
            steps {
                powershell(script: 'dotnet test $env:TEST_PATH')
            }
        }
        stage('Publish') {
            steps {
                powershell(script: 'dotnet publish -o ./Published')
            }
        }
    }
}
