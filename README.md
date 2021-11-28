## Instruction
---
1. Build the image
```
docker build -t policyapi .
```
2. Run the container
```
docker run -p 8080:80 --name policyapi policyapi
```

## Endpoints
---

**Base URL:** `http://localhost:8080/`

* `GET /api/Policy`
> Get all policies

* `GET /api/Policy/{id}`
> Get policy by id

* `POST /api/Policy`
```json
{
    "product": {
        "name": "<productType>",
        "type": "<productName>"
    },
    "party": {
        "givenName": "<firstName>",
        "surname": "<surname>",
        "birthDate": "<isoDate>"
    }
}

```
> Save policy

* `PUT /api/Policy/{id}`
```json
{
    "id": <id>
    "product": {
        "name": "<productType>",
        "type": "<productName>"
    },
    "party": {
        "givenName": "<firstName>",
        "surname": "<surname>",
        "birthDate": "<isoDate>"
    }
}
```
> Update policy

* `DELETE /api/Policy/{id}`
> Delete policy
