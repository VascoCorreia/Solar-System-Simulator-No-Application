const { Client } = require('pg')

const client = new Client({
    host: "localhost",
    user: "postgres",
    port: 5432,
    password: "wowgold1",
    database: "SolarSystem"
})

module.exports = client
