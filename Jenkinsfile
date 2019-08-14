pipeline 
{
    environment
    {
        GIT_HTTPS_PATH = "https://github.com/tavisca-vraut/DemoWebAPI.git"
        SOLUTION_FILE_PATH = "DemoWebApp.sln"
        TEST_FILE_PATH = "DemoTest/DemoTest.csproj"
    }
    parameters
    {

    }
    agent any
    stages
    {
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