import {useState, useEffect} from 'react';

const useFilter = (properties) => {
    const [orderBy, setOrderBy] = useState(properties[0].id);
    const [order, setOrder] = useState('desc');
    const [filterQuery, setFilterQuery] = useState('');
    const [urlSearchParams, setUrlSearchParams] = useState(new URLSearchParams())

    const handleFilterByQuery = (event) => {
        setFilterQuery(event.target.value);
    };

    const handleOrderByChange = (event) => {
        setOrderBy(event.target.value);
    };

    const handleOrderChange = (event, newOrder) => {
        if (newOrder) setOrder(newOrder);
    };

    useEffect(() => {
        setUrlSearchParams(new URLSearchParams({
            sort: order,
            param: orderBy,
            query: filterQuery
        }))
    }, [order, orderBy, filterQuery])

    return [urlSearchParams,
        orderBy, handleOrderByChange,
        order, handleOrderChange,
        filterQuery, handleFilterByQuery, setFilterQuery
    ];
};

export default useFilter;