export default {
    async get(url, data) {
        const response = await fetch(url, {
            method: 'GET',
            body: JSON.stringify(data),
            credentials: 'include'
        });
        if (!response.ok && response.status === 401) {
            window.location.href = '#/not-allowed';
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
            window.location.href = '#/not-allowed';
            return;
        }

        return response.json();
    }
}