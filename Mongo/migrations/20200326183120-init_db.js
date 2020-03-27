module.exports = {
    async up(db, client) {
        try {
            await db.createCollection('Meetings');

            await db.createCollection('Users', {
              validator: {
                $jsonSchema: {
                  bsonType: "object",
                  required: ["name", "email"],
                  properties: {
                    name: {
                      bsonType: "string"
                    },
                    email: {
                      bsonType: "string"
                    }
                  }
                }
              }
            });
            
        } catch (e) {
            throw e;
        }
    },

    async down(db, client) {
        try {
            await db.dropCollection('Meetings');
            await db.dropCollection('Users');
        } catch (e) {
            throw e;
        }
    }
};
