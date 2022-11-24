import {useState} from 'react';
import {useNavigate} from 'react-router-dom';
// material
import {
    Card,
    Table,
    Checkbox,
    TableRow,
    TableBody,
    TableCell,
    TableContainer,
    TablePagination, Box, Grow
} from '@mui/material';
// components
import * as React from "react";
import useFetch from "../../utils/fetch";
import {formatDate, getTicketsUri, SEVERITY, STATUS} from "../../utils/constants";
import PageLoading from "../common/PageLoading";
import SearchNotFound from "./SearchNotFound";
import Label from "../common/Label";
import {applySortFilter, getComparator, ListHead, ListToolbar, MoreMenu} from "../table";

// ----------------------------------------------------------------------

const TABLE_HEAD = [
    {id: 'propertyName', label: 'Property', alignRight: false},
    {id: 'unitNo', label: 'Unit', alignRight: false},
    {id: 'problem', label: 'Problem', alignRight: false},
    {id: 'description', label: 'Description', alignRight: false},
    {id: 'createdOn', label: 'Created', alignRight: false},
    {id: 'estimatedDate', label: 'Estimated', alignRight: false},
    {id: 'severity', label: 'Severity', alignRight: false},
    {id: 'status', label: 'Status', alignRight: false},
    {id: ''}
];

// ----------------------------------------------------------------------
/**
 * Table overview for Tickets page.
 * @returns {JSX.Element}
 * @constructor
 */
export default function TableOverview() {
    const navigate = useNavigate();
    // CONFIG ---------------------------------------------------------------
    const title = "Tickets"
    const filterData = (data) => {
        data.forEach((d) => {
            d.propertyName = d.unit.property.address
            d.unitNo = d.unit.unitNo
            d.createdOn = new Date(d.createdOn)
            if (d.estimatedDate) d.estimatedDate = new Date(d.estimatedDate)
        })
        return data;
    }
    const properties = TABLE_HEAD.slice(0, -1);
    const dataName = 'Ticket';
    const dataId = 'ticketId';
    const [filterQueryProperty, setFilterQueryProperty] = useState('propertyName')
    const [orderBy, setOrderBy] = useState('status');
    const [data, errorData, loadingData] = useFetch('/api/Tickets', filterData);
    const uri = getTicketsUri;
    // ----------------------------------------------------------------------

    const [page, setPage] = useState(0);
    const [order, setOrder] = useState('asc');
    const [selected, setSelected] = useState([]);
    const [filterQuery, setFilterQuery] = useState('');
    const [rowsPerPage, setRowsPerPage] = useState(5);

    const handleRequestSort = (event, property) => {
        const isAsc = orderBy === property && order === 'asc';
        setOrder(isAsc ? 'desc' : 'asc');
        setOrderBy(property);
    };

    const handleSelectAllClick = (event) => {
        if (event.target.checked) {
            const newSelecteds = data.map((n) => n[dataId]);
            setSelected(newSelecteds);
            return;
        }
        setSelected([]);
    };

    const handleClick = (event, property) => {
        const selectedIndex = selected.indexOf(property);
        let newSelected = [];
        if (selectedIndex === -1) {
            newSelected = newSelected.concat(selected, property);
        } else if (selectedIndex === 0) {
            newSelected = newSelected.concat(selected.slice(1));
        } else if (selectedIndex === selected.length - 1) {
            newSelected = newSelected.concat(selected.slice(0, -1));
        } else if (selectedIndex > 0) {
            newSelected = newSelected.concat(
                selected.slice(0, selectedIndex),
                selected.slice(selectedIndex + 1)
            );
        }
        setSelected(newSelected);
    };

    const handleChangePage = (event, newPage) => {
        setPage(newPage);
    };

    const handleChangeRowsPerPage = (event) => {
        setRowsPerPage(parseInt(event.target.value, 10));
        setPage(0);
    };

    const handleFilterByQuery = (event) => {
        setFilterQuery(event.target.value);
    };

    const emptyRows = page > 0 ? Math.max(0, (1 + page) * rowsPerPage - data.length) : 0;

    const filteredData = applySortFilter(data, getComparator(order, orderBy), filterQuery, filterQueryProperty);

    const isDataNotFound = filteredData.length === 0 && data.length > 0;

    const noData = data.length === 0;

    return (
        <>
            <PageLoading loadingData={loadingData}/>
            <Grow in={!loadingData}>
                <Card sx={{display: loadingData ? 'none' : undefined}}>
                    {/* For enabling local search bar and select, uncomment: */}
                    {/*<ListToolbar*/}
                    {/*    numSelected={selected.length}*/}
                    {/*    filterQuery={filterQuery}*/}
                    {/*    onFilterQuery={handleFilterByQuery}*/}
                    {/*    properties={properties}*/}
                    {/*    filterQueryProperty={filterQueryProperty}*/}
                    {/*    setFilterQueryProperty={setFilterQueryProperty}*/}
                    {/*    setFilterQuery={setFilterQuery}*/}
                    {/*/>*/}
                    <TableContainer>
                        <Table>
                            <ListHead
                                order={order}
                                orderBy={orderBy}
                                headLabel={TABLE_HEAD}
                                rowCount={data.length}
                                numSelected={selected.length}
                                onRequestSort={handleRequestSort}
                                onSelectAllClick={handleSelectAllClick}
                            />
                            <TableBody>
                                {filteredData
                                    .slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
                                    .map((row) => {
                                        const {
                                            createdOn,
                                            description,
                                            estimatedDate,
                                            problem,
                                            severity,
                                            status,
                                            ticketId,
                                            unitNo,
                                            propertyName
                                        } = row;
                                        const isItemSelected = selected.indexOf(ticketId) !== -1;

                                        return (
                                            <TableRow
                                                hover
                                                key={ticketId}
                                                tabIndex={-1}
                                                role="checkbox"
                                                selected={isItemSelected}
                                                aria-checked={isItemSelected}
                                                onClick={() => {
                                                    navigate(`/dashboard/${uri(row)}`);
                                                }}
                                            >
                                                <TableCell padding="checkbox">
                                                    <Checkbox
                                                        checked={isItemSelected}
                                                        onChange={(event) => handleClick(event, ticketId)}
                                                    />
                                                </TableCell>
                                                <TableCell align="left">{propertyName}</TableCell>
                                                <TableCell align="left">{unitNo}</TableCell>
                                                <TableCell align="left">{problem}</TableCell>
                                                <TableCell align="left">{description}</TableCell>
                                                <TableCell
                                                    align="left">{formatDate(createdOn)} </TableCell>
                                                <TableCell
                                                    align="left">{estimatedDate ? formatDate(estimatedDate) : null}</TableCell>
                                                <TableCell align="left">
                                                    <Label
                                                        variant="ghost"
                                                        color={SEVERITY[severity].color}
                                                    >
                                                        {SEVERITY[severity].text}
                                                    </Label>
                                                </TableCell>
                                                <TableCell align="left">
                                                    <Label
                                                        variant="ghost"
                                                        color={STATUS[status].color}
                                                    >
                                                        {STATUS[status].text}
                                                    </Label>
                                                </TableCell>

                                                <TableCell align="right">
                                                    <MoreMenu/>
                                                </TableCell>
                                            </TableRow>
                                        );
                                    })}
                                {emptyRows > 0 && (
                                    <TableRow style={{height: 53 * emptyRows}}>
                                        <TableCell colSpan={6}/>
                                    </TableRow>
                                )}
                            </TableBody>

                            {isDataNotFound && (
                                <TableBody>
                                    <TableRow>
                                        <TableCell align="center" colSpan={TABLE_HEAD.length} sx={{py: 3}}>
                                            <SearchNotFound searchQuery={filterQuery} sx={{width: '100%'}}/>
                                        </TableCell>
                                    </TableRow>
                                </TableBody>
                            )}
                            {noData && (
                                <TableBody>
                                    <TableRow>
                                        <TableCell align="center" colSpan={TABLE_HEAD.length} sx={{py: 3}}>
                                            <Box sx={{
                                                color: 'gainsboro'
                                            }}>{`No ${title}`}</Box>
                                        </TableCell>
                                    </TableRow>
                                </TableBody>
                            )}
                        </Table>
                    </TableContainer>
                    {filteredData.length > 4 ?
                        <TablePagination
                            rowsPerPageOptions={[5, 10, 25]}
                            component="div"
                            count={filteredData.length}
                            rowsPerPage={rowsPerPage}
                            page={page}
                            onPageChange={handleChangePage}
                            onRowsPerPageChange={handleChangeRowsPerPage}
                        /> : <Box height={15}></Box>
                    }
                </Card>
            </Grow>
        </>
    );
}
