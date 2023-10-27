# Rehber Kaydı ve Raporlaması
-Rehbere kişi adı soyadı ve lokasyon bilgisini kaydedip istenildiğinde raporlamasını sağlanmıştır\
-Birbirleriyle Mesaj broker aracılığıyla haberleşebilen microservis yapısı inşa edilmiştir.\
-ContactAPI projesinde Onion Katmanlı mimari kullanılmıştır.CQRS pattern uygulanmıştır.
## Çalıştırma 
`docker compose up` : Bu komutla projeyi docker aracılığıyla ayağa kaldırabilirsiniz.\
`docker compose down` : Çalışan projeyi durdurup containerları silebilirsiniz

## Base EndPoints
`ContactApi{BASE_CONTACT_API}: http://localhost:8002` \
`ContactReportApi(BASE_CONTANT_REPORT_API) : http://localhost:8001`\
`Api Gateway (BASE_APIGATEWAY) : http://localhost:5001`

### ReportAPI Endpoints
`Swagger` : http://localhost:8001/swagger/index.html


### ContactAPI Endpoints
`Swagger` :http://localhost:8002/swagger/index.html

# Mimari

![Mimari](https://github.com/adnanarslangiray/ContactRegistry/assets/33246502/50ceb044-d23b-4237-9e19-f32df89b5053)
