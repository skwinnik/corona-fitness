export default {
    async get(url, data) {
        let queryString = '';
        if (data)
            queryString = '?' + Object.entries(data).map(x => x[0] + '=' + x[1]).join('&');
        
        const response = await fetch(url + queryString, {
            method: 'GET',
            credentials: 'include'
        });
        if (!response.ok && response.status === 401) {
            return;
        }
        
        return response.json();
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

        if (!response.ok && response.status === 401) {
            return;
        }

        return response.json();
    }
}