import {filter} from "lodash";
import {formatDate} from "../../utils/constants";

export function descendingComparator(a, b, orderBy) {
    if (b[orderBy] < a[orderBy]) {
        return -1;
    }
    if (b[orderBy] > a[orderBy]) {
        return 1;
    }
    return 0;
}

export function getComparator(order, orderBy) {
    return order === 'desc'
        ? (a, b) => descendingComparator(a, b, orderBy)
        : (a, b) => -descendingComparator(a, b, orderBy);
}

export function applySortFilter(array, comparator, query, filterProperty) {
    const stabilizedThis = array.map((el, index) => [el, index]);
    stabilizedThis.sort((a, b) => {
        const order = comparator(a[0], b[0]);
        if (order !== 0) return order;
        return a[1] - b[1];
    });
    if (query) {
        return filter(array, (data) => {
            if (typeof data[filterProperty] == 'string') {
                return data[filterProperty].toLowerCase().indexOf(query.toLowerCase()) !== -1
            } else if (data[filterProperty] instanceof Date) {
                return formatDate(data[filterProperty]).toLowerCase().includes(query.toLowerCase());
            } else {
                return data[filterProperty].toString().toLowerCase().includes(query.toLowerCase());
            }
        });

    }
    return stabilizedThis.map((el) => el[0]);
}