# Rehber Kaydı ve Raporlaması
-Rehbere kişi adı soyadı ve lokasyon bilgisini kaydedip istenildiğinde raporlamasını sağlanmıştır\
-Birbirleriyle Mesaj broker aracılığıyla haberleşebilen microservis yapısı inşa edilmiştir.\
-ContactAPI projesinde Onion Katmanlı mimari kullanılmıştır.CQRS pattern uygulanmıştır.
## Çalıştırma (How to Run)
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

### ApiGateway(Ocelot Api) Rota Tanımları
API rotalarının tanımları ve ilişkilendirilmiş HTTP metotları bulunmaktadır.

## Contacts API

- **Rota 1**
  - UpstreamPathTemplate: `/contacts`
  - UpstreamHttpMethod: GET, POST, PUT
  - DownstreamPathTemplate: `/api/v1/contacts`

- **Rota 2**
  - UpstreamPathTemplate: `/contacts/contacts/{id}`
  - UpstreamHttpMethod: GET, DELETE
  - DownstreamPathTemplate: `/api/v1/contacts/{id}`

## Contact Features API

- **Rota 1**
  - UpstreamPathTemplate: `/contact-features`
  - UpstreamHttpMethod: GET, POST
  - DownstreamPathTemplate: `/api/v1/contact-features`

- **Rota 2**
  - UpstreamPathTemplate: `/contact-features/{id}`
  - UpstreamHttpMethod: GET, POST, DELETE
  - DownstreamPathTemplate: `/api/v1/contact-features/{id}`

## Reports API

- **Rota 1**
  - UpstreamPathTemplate: `/reports`
  - UpstreamHttpMethod: GET, POST
  - DownstreamPathTemplate: `/api/reports`

- **Rota 2**
  - UpstreamPathTemplate: `/reports/{id}`
  - UpstreamHttpMethod: GET, DELETE
  - DownstreamPathTemplate: `/api/reports/{id}`

## Report Details API

- **Rota 1**
  - UpstreamPathTemplate: `/report-details`
  - UpstreamHttpMethod: GET, POST
  - DownstreamPathTemplate: `/api/report-details`

- **Rota 2**
  - UpstreamPathTemplate: `/report-details/{id}`
  - UpstreamHttpMethod: GET, DELETE
  - DownstreamPathTemplate: `/api/report-details/{id`

## Global Configuration

- Base URL: `http://contactregistry.apigateway:5000`


# Mimari (Project Architecture)


![prjarc](https://github.com/adnanarslangiray/ContactRegistry/assets/33246502/790242ad-fe55-42e6-b682-a3fddcba88fb)


