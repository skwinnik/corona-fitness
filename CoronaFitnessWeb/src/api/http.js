export default {
    async get(url, data) {
        const response = await fetch(url, {
            method: 'GET',
            credentials: 'include'
        });
        if (response.ok)
            return response;

        throw response;
    },

    async post(url, data) {
        const response = await fetch(url, {
            method: 'POST',
            body: JSON.stringify(data),
            headers: {
                'Content-Type': 'application/json'
            },
            credentials: 'include'
        });
        if (response.ok)
            return response;
        
        throw response;
    },

    async put(url, data) {
        const response = await fetch(url, {
            method: 'PUT',
            body: JSON.stringify(data),
            headers: {
                'Content-Type': 'application/json'
            },
            credentials: 'include'
        });

        if (response.ok)
            return response;

        throw response;
    },

    async delete(url, data) {
        const response = await fetch(url, {
            method: 'DELETE',
            body: JSON.stringify(data),
            headers: {
                'Content-Type': 'application/json'
            },
            credentials: 'include'
        });

        if (response.ok)
            return response;

        throw response;
    }
}