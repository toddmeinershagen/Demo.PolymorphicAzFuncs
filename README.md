1.  Get All Pay Runs

GET /api/client/1/payroll/2/paygroup/3/payrun HTTP/1.1
Host: localhost:8081


2.  Get Single Pay Run

GET /api/client/1/payroll/2/paygroup/3/payrun/{payrunId} HTTP/1.1
Host: localhost:8081


3.  Post Regular Pay Run

POST /api/client/1/payroll/2/paygroup/3/payrun HTTP/1.1
Host: localhost:8081
Content-Type: text/plain

{
    "type": "regular",
    "checkDate": "2020-06-02",
}


4.  Post Adjustment Pay Run

POST /api/client/1/payroll/2/paygroup/3/payrun HTTP/1.1
Host: localhost:8081
Content-Type: text/plain

{
    "type": "adjustment",
    "checkDate": "2020-06-02",
    "billingReason":  "This is the reason"
}
