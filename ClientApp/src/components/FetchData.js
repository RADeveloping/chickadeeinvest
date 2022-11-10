import {useState, useEffect} from 'react';

const useFetch = (url, filter, reset) => {
    const [data, setData] = useState([]);
    const [error, setError] = useState('');
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        if (url) {
            console.log(url)
            fetch(url)
                .then((res) => res.json())
                .then((data) => {
                    if (!data) data = []
                    if (data.status && data.status === 404) data = []
                    if (Array.isArray(data) && filter) data = filter(data);
                    setData(data);
                    setLoading(false);
                })
                .catch((err) => {
                    console.error(err);
                    setError(err);
                    setData([])
                    setLoading(false);
                });
        } else {
            setData([])
            setLoading(false);
        }
    }, [url]);

    return [data, error, loading];
};

export default useFetch;
