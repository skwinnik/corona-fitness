module.exports = {
    async up(db, client) {
        try {
            await db.createCollection('Meetings', {
                validator: {
                    $jsonSchema: {
                        bsonType: "object",
                        required: ["title", "description", "ownerId", "attendees"],
                        properties: {
                            title: {
                                bsonType: "string"
                            },
                            description: {
                                bsonType: "string"
                            },
                            ownerId: {
                                bsonType: "objectId"
                            },
                            attendees: {
                                bsonType: "array"
                            }
                        }
                    }
                }
            });

            await db.createCollection('Users', {
                validator: {
                    $jsonSchema: {
                        bsonType: "object",
                        required: ["name", "email", "identityId"],
                        properties: {
                            name: {
                                bsonType: "string"
                            },
                            email: {
                                bsonType: "string"
                            },
                            identityId: {
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
