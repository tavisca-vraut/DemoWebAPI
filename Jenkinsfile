pipeline {
    agent any
    parameters {
        string(name: 'REPO_PATH', defaultValue: 'https://github.com/tavisca-vraut/DemoWebAPI.git')
        string(name: 'SOLUTION_PATH', defaultValue: 'DemoWebApp.sln')
        string(name: 'TEST_PATH', defaultValue: 'DemoTest/DemoTest.csproj')
        choice(name: 'JOB', choices:  ['Build' , 'Test'])
        string(name: 'PROJECT', defaultValue: 'DemoWebApp')
    }
    stages {
        
        stage('Build') {
            steps {
                sh 'dotnet restore ${SOLUTION_PATH} --source https://api.nuget.org/v3/index.json'
                sh 'dotnet build ${SOLUTION_PATH} -p:Configuration=release -v:n'
            }
        }
        stage('Test') {
            when
            {
                expression { params.JOB == 'Test' }
            }
            
            steps {
                sh 'dotnet test ${TEST_PATH}'
            }
        }
        stage('Publish') {
            steps {
                sh 'dotnet publish -o ./Published'
            }
        }
    }
}
