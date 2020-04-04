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
                            sessionId: {
                                bsonType: "string"
                            },
                            attendees: {
                                bsonType: ["array"],
                                description: 'list of meeting attendees',
                                items: {
                                    bsonType: 'object',
                                    required: ['userId', 'role'],
                                    properties: {
                                        userId: {
                                            bsonType: 'objectId'
                                        },
                                        role: {
                                            bsonType: 'string'
                                        },
                                        token: {
                                            bsonType: 'string'
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            });

            await db.createCollection('Users', {
                validator: {
                    $jsonSchema: {
                        bsonType: "object",
                        required: ["name", "email", "identityId", "canCreateMeetings"],
                        properties: {
                            name: {
                                bsonType: "string"
                            },
                            email: {
                                bsonType: "string"
                            },
                            identityId: {
                                bsonType: "string"
                            },
                            canCreateMeetings: {
                                bsonType: "bool"
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
