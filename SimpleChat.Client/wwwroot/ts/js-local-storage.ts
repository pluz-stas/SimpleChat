function set(key: string, value: string) {localStorage.setItem(key, value); }
function get(key: string) { return localStorage.getItem(key); }
function remove(key: string) { return localStorage.removeItem(key); }