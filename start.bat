docker compose up -d --build

dotnet tool install --global dotnet-ef --version 7.0.14

cd src/VL.Challenge.API
dotnet ef database update --project ../VL.Challenge.Storage -- localhost
cd ../..