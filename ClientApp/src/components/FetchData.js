import { useState, useEffect } from 'react';
const useFetch = (url) => {
  const [data, setData] = useState([]);
  const [error, setError] = useState('');
  
  useEffect(async () => {
    await fetch(url)
    .then(res => res.json())
    .then((data) => {
      setData(data);
    })
    .catch((err) => {
      console.error(err);
      setError(err);
    })
  }, [url]);
  return { data, error };
}

export default useFetch;