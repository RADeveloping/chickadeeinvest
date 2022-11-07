import {useState, useEffect} from 'react';

const useFetch = (url, filter) => {
    const [data, setData] = useState([]);
    const [error, setError] = useState('');
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        if (url) {
            fetch(url)
                .then((res) => res.json())
                .then((data) => {
                    if (!Array.isArray(data)) data = []
                    if (filter) data = filter(data);
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
