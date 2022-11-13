import {useState, useEffect} from 'react';
let controller = new AbortController();
/**
 * Aborts all fetches using useFetch.
 */
export const abortFetch = () => {
    controller.abort();
    controller = new AbortController();
}
/**
 * Global fetch that returns common hooks for fetching.
 * @param url {string} URL to fetch.
 * @param [filter = false] {(a:[])=>void} Method to filter data before return.
 * @param [reset = false] {boolean} Resets loading on new fetch.
 * @returns {[*[],string,boolean]}
 */
const useFetch = (url, filter, reset) => {
    const [data, setData] = useState([]);
    const [error, setError] = useState('');
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        if (url) {
            if (reset) setLoading(true)
            console.log(url)
            fetch(url, {signal: controller.signal})
                .then((res) => res.json())
                .then((data) => {
                    if (!data) data = []
                    if (data.status && data.status === 404) data = []
                    if (Array.isArray(data) && filter) data = filter(data);
                    setData(data);
                    setLoading(false);
                })
                .catch((err) => {
                    console.log(err);
                    setError(err);
                    setData([])
                    setLoading(false);
                });
        } else {
            setData([])
            if (!reset) setLoading(false);
        }
    }, [url]);

    return [data, error, loading];
};

export default useFetch;
