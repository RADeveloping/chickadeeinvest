import {useState, useEffect} from 'react';

const useFetch = (url, filter) => {
    const [data, setData] = useState([]);
    const [error, setError] = useState('');
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        if (url) {
            setLoading(true)
            fetch(url)
                .then((res) => res.json())
                .then((data) => {
                    if (Array.isArray(data) && filter) {
                        console.log(data)
                        data = filter(data);
                    } else {
                        data = []
                    }
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
