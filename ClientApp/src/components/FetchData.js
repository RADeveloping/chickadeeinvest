import { useState, useEffect } from 'react';
export function useFetch(url) {
  const [data, setData] = useState([]);
  const [error, setError] = useState('');

  useEffect(() => {
    fetch(url)
      .then((res) => res.json())
      .then((json) => {
        setData(json);
      })
      .catch((err) => {
        console.error(err);
        setError(err);
      });
  }, [url]);
  return { data, error };
};
