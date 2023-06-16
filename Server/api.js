const client = require('./connection.js')
const express = require('express');
const bodyParser = require("body-parser");

const app = express();

app.use(bodyParser.json());
app.use(express.urlencoded({ extended: true }));

app.listen(3300, () => {
    console.log("Sever is now listening at port 3300");
})

app.get("/users", (req, res) => {
    client.query("Select * from users order by id asc", (err, results) => {
        if (err)
            throw err
        else
            res.status(200).send(results.rows);
    })
})

app.post("/register", (req, res, next) => {

    var username = req.body.username;
    var password = req.body.password;

    client.query('SELECT username FROM users WHERE username= $1', [username], (err, result) => {

        if (err)
            throw err;

        else if (result.rowCount == 0) {
            client.query('INSERT INTO users (username, password) VALUES ($1, $2)', [username, password], (err, result) => {
                if (err)
                    throw err;

                else {
                    res.status(200).send();
                }
            })
        }
        else {
            res.status(400).send();
        }
    })
})

app.post("/login", (req, res) => {

    const username = req.body.username;
    const password = req.body.password;

    if (username && password) {

        client.query("SELECT * FROM users WHERE username = $1", [username], (err, result) => {

            if (err) {
                throw error;
            }

            else if (result.rowCount == 0) {
                res.status(400).send()
            }

            else {
                client.query('SELECT username, password FROM users WHERE username = $1', [username], (err, result) => {

                    if (err) {
                        throw err;
                    }

                    if (result.rows[0].password == password) {

                        const objToSend =
                        {
                            username: result.rows[0].username,
                            password: result.rows[0].password
                        }
                        //format response correctly in a JSON

                        res.status(200).send(JSON.stringify(objToSend));
                    }
                    else
                        res.status(400).send()
                })
            }
        })
    }

    else {
        res.status(400).send();
        response.end();
    }
});

app.get('/getPlanetData', (req, res) =>
{
    let id = req.query.id;
    client.query('SELECT * FROM celestialBodies WHERE id = $1', [id], (err, result) =>
    {
        if (err)
            throw err;
        else {
            let objToSend =
            {
                id: result.rows[0].id,
                name: result.rows[0].name,
                mass: result.rows[0].mass,
                diameter: result.rows[0].diameter,
                rotationTime: result.rows[0].rotationTime,
                initialVelocity: result.rows[0].initialVelocity,
                tilt: result.rows[0].tilt
            }

            res.send(JSON.stringify(objToSend));
        }
    })
});

app.get('/getAllPlanetsData', (req, res) => {

    data = []

    client.query('SELECT * FROM celestialBodies ORDER BY id ASC', (err, result) => {
        if (err)
            throw err;
        else {
            for (i = 0; i < result.rowCount; i++) {
                data.push(result.rows[i]);
            }

            res.send(data);
        }
    })
});

app.post('/updatePlanetMass', (req, res) => {

    let newPlanetMass = req.body.newPlanetMass;
    let planetId = req.body.id

    client.query('UPDATE celestialBodies SET mass = $1 WHERE id = $2', [newPlanetMass, planetId], (err, result) => {
        if (err)
            throw err;
        else {

            res.status(200).send();
        }
    })
});

app.post('/resetGame', (req, res) => {

    client.query(`UPDATE celestialBodies as c SET
                  mass = u2.mass
                  FROM(values
                        (1, 198910), 
                        (2, 0.0330220013856888),
                        (3, 0.48685),
                        (4, 0.59736),
                        (5, 0.064185),
                        (6, 189.86),
                        (7, 56.846),
                        (8, 8.681),
                        (9, 12.43)
                      ) as u2(id, mass)           
                  WHERE u2.id = c.id;`, (err, result) => {
        if (err)
            throw err;
        else {

            res.status(200).send();
        }
    })
})


client.connect();