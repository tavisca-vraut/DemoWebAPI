pipeline 
{
    environment
    {
        PROJECT = "DemoWebAPI"
        GIT_HTTPS_PATH = "https://github.com/tavisca-vraut/${PROJECT}.git"
        SOLUTION_FILE_PATH = "DemoWebApp.sln"
        TEST_FILE_PATH = "DemoTest/DemoTest.csproj"
    }
    agent any
    stages
    {
        stage(construction)
        {
            sh "git clone ${GIT_HTTPS_PATH}"
            sh "cd ${PROJECT}"
        }
        stage(build)
        {
            steps
            {
                sh "dotnet restore ${SOLUTION_FILE_PATH} --source https://api.nuget.org/v3/index.json"
                sh "dotnet build ${SOLUTION_FILE_PATH} -p:Configuration=release -v:n"
            }
        }
        stage(test)
        {
            steps
            {
                sh "dotnet test ${TEST_FILE_PATH}"
            }
        }
    }
}