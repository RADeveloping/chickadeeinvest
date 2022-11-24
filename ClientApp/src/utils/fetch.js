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
 * @param [callBack = false] {function} Callback to be called after completion.
 * @param [resetOnNull = false] {boolean} Reset loading on null url in addition to at load.
 * @returns {[*[],string,boolean,function]}
 */
const useFetch = (url, filter, reset, callBack, resetOnNull) => {
    const [data, setData] = useState([]);
    const [error, setError] = useState('');
    const [loading, setLoading] = useState(true);
    const [reload, setReload] = useState(0);

    const reloadFetch = () => {
        setReload(reload + 1);
    }

    useEffect(() => {
        if (url) {
            if (reset || resetOnNull) setLoading(true)
            console.log(url)
            fetch(url, {signal: controller.signal})
                .then((res) => res.json())
                .then((data) => {
                    if (callBack) callBack();
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
            if (!reset || resetOnNull) setLoading(false);
        }
    }, [url, reload]);

    return [data, error, loading, reloadFetch];
};

/**
 * Global fetch for put and post and returns common hooks for fetching.
 * @param url {string} URL to fetch.
 * @param [post = false] {Object} Object to be posted, should be a state set to null.
 * @param [patch = false] {Object} Object to be patched, should be a state set to null.
 * @param [callBack = false] {function} Callback to be called after completion.
 * @returns {[*[],string,boolean]}
 */
export const usePost = (url, post, patch, callBack) => {
    const [resp, setResp] = useState([]);
    const [error, setError] = useState('');
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        if (url && (post || patch)) {
            setLoading(true);
            console.log(url)
            fetch(url, {
                method: post ? 'POST' : patch ? 'PATCH' : undefined,
                headers: {
                    'Content-Type': 'application/json',
                },
                body: post ? JSON.stringify(post) : patch ? JSON.stringify(patch) : undefined
            })
                .then((res) => res.json())
                .then((data) => {
                    if (callBack) callBack();
                    setResp(data);
                    setLoading(false);
                })
                .catch((err) => {
                    console.log(err);
                    setError(err);
                    setLoading(false);
                });
        } else {
            setLoading(false);
        }
    }, [url, post, patch]);

    return [resp, error, loading];
}

export default useFetch;
