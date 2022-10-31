import { useState, useEffect } from 'react';
const useFetch = (url, filter) => {
  const [data, setData] = useState([]);
  const [error, setError] = useState('');
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    fetch(url)
      .then((res) => res.json())
      .then((data) => {
        if (data.length > 0 && filter) {
          data = filter(data);
        }
        setData(data);
        setLoading(false);
      })
      .catch((err) => {
        console.error(err);
        setError(err);
        setLoading(false);
      });
  }, [url]);
  return [data, error, loading];
};

export default useFetch;
