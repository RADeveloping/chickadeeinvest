import { filter, forEach } from 'lodash';
import { sentenceCase } from 'change-case';
import {useEffect, useState} from 'react';
import { Link as RouterLink } from 'react-router-dom';
// material
import {
  Card,
  Table,
  Stack,
  Avatar,
  Button,
  Checkbox,
  TableRow,
  TableBody,
  TableCell,
  Container,
  Typography,
  TableContainer,
  TablePagination, CircularProgress, Box, Fade, Grow
} from '@mui/material';
// components
import Page from '../components/Page';
import Label from '../components/Label';
import Scrollbar from '../components/Scrollbar';
import Iconify from '../components/Iconify';
import SearchNotFound from '../components/SearchNotFound';
import { ListHead, ListToolbar, MoreMenu } from '../sections/@dashboard/list';
// mock
import useFetch from "../components/FetchData";

// ----------------------------------------------------------------------

const TABLE_HEAD = [
  { id: 'problem', label: 'Problem', alignRight: false },
  { id: 'description', label: 'Description', alignRight: false },
  { id: 'createdOn', label: 'Created', alignRight: false },
  { id: 'estimatedDate', label: 'Estimated', alignRight: false },
  { id: 'severity', label: 'Severity', alignRight: false },
  { id: 'status', label: 'Status', alignRight: false },
  { id: '' }
];

const SEVERITY = {
  0: {color: 'success', text: 'Low'},
  1: {color: 'warning', text: 'Medium'},
  2: {color: 'error', text: 'High'}
}

const STATUS = {
  0: {color: 'info', text: 'Open'},
  1: {color: 'primary', text: 'Closed'},
}

// ----------------------------------------------------------------------

function descendingComparator(a, b, orderBy) {
  if (b[orderBy] < a[orderBy]) {
    return -1;
  }
  if (b[orderBy] > a[orderBy]) {
    return 1;
  }
  return 0;
}

function getComparator(order, orderBy) {
  return order === 'desc'
    ? (a, b) => descendingComparator(a, b, orderBy)
    : (a, b) => -descendingComparator(a, b, orderBy);
}

function applySortFilter(array, comparator, query) {
  const stabilizedThis = array.map((el, index) => [el, index]);
  stabilizedThis.sort((a, b) => {
    const order = comparator(a[0], b[0]);
    if (order !== 0) return order;
    return a[1] - b[1];
  });
  if (query) {
    return filter(array, (ticket) => ticket.problem.toLowerCase().indexOf(query.toLowerCase()) !== -1);
  }
  return stabilizedThis.map((el) => el[0]);
}

export default function Tickets() {
  const filterTicket = (data) => {
    data.forEach((d)=> {
      d.createdOn = new Date(d.createdOn)
      d.estimatedDate = new Date(d.estimatedDate)
    })
    return data;
  }
  
  const [data, errorData, loadingData] = useFetch('/api/Ticket', filterTicket)

  const [page, setPage] = useState(0);

  const [order, setOrder] = useState('desc');

  const [selected, setSelected] = useState([]);

  const [orderBy, setOrderBy] = useState('createdOn');

  const [filterName, setFilterName] = useState('');

  const [rowsPerPage, setRowsPerPage] = useState(5);

  const handleRequestSort = (event, property) => {
    const isAsc = orderBy === property && order === 'asc';
    setOrder(isAsc ? 'desc' : 'asc');
    setOrderBy(property);
  };

  const handleSelectAllClick = (event) => {
    if (event.target.checked) {
      const newSelecteds = data.map((n) => n.name);
      setSelected(newSelecteds);
      return;
    }
    setSelected([]);
  };

  const handleClick = (event, name) => {
    const selectedIndex = selected.indexOf(name);
    let newSelected = [];
    if (selectedIndex === -1) {
      newSelected = newSelected.concat(selected, name);
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

  const handleFilterByName = (event) => {
    setFilterName(event.target.value);
  };

  const emptyRows = page > 0 ? Math.max(0, (1 + page) * rowsPerPage - data.length) : 0;

  const filteredData = applySortFilter(data, getComparator(order, orderBy), filterName);

  const isDataNotFound = filteredData.length === 0;

  return (
    <Page title={"Tickets"}>
      <Container>
        <Stack direction="row" alignItems="center" justifyContent="space-between" mb={5}>
          <Typography variant="h4" gutterBottom>
            Tickets
          </Typography>
          {/*<Button*/}
          {/*  variant="contained"*/}
          {/*  component={RouterLink}*/}
          {/*  to="#"*/}
          {/*  startIcon={<Iconify icon="eva:plus-fill" />}*/}
          {/*>*/}
          {/*  New User*/}
          {/*</Button>*/}
        </Stack>
        {loadingData ?
            <Box   display="flex"
                   justifyContent="center"
                   alignItems="center"
            height="50vh">
              <CircularProgress />
            </Box> : null }
        <Grow in={!loadingData}>
        <Card sx={{display: loadingData ? 'none' : undefined}}>
          <ListToolbar
            numSelected={selected.length}
            filterName={filterName}
            onFilterName={handleFilterByName}
          />
   
 
            <TableContainer >
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
                      const { createdOn, description, estimatedDate, problem, severity, status, ticketId } = row;
                      const isItemSelected = selected.indexOf(ticketId) !== -1;

                      return (
                        <TableRow
                          hover
                          key={ticketId}
                          tabIndex={-1}
                          role="checkbox"
                          selected={isItemSelected}
                          aria-checked={isItemSelected}
                        >
                          <TableCell padding="checkbox">
                            <Checkbox
                              checked={isItemSelected}
                              onChange={(event) => handleClick(event, ticketId)}
                            />
                          </TableCell>
                          <TableCell align="left">{problem}</TableCell>
                          <TableCell align="left">{description}</TableCell>
                          <TableCell align="left">{createdOn.toLocaleDateString('en-CA', {dateStyle: 'medium'})} </TableCell>
                          <TableCell align="left">{estimatedDate.toLocaleDateString('en-CA', {dateStyle: 'medium'})}</TableCell>
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
                            <MoreMenu />
                          </TableCell>
                        </TableRow>
                      );
                    })}
                  {emptyRows > 0 && (
                    <TableRow style={{ height: 53 * emptyRows }}>
                      <TableCell colSpan={6} />
                    </TableRow>
                  )}
                </TableBody>

                {isDataNotFound && (
                  <TableBody>
                    <TableRow>
                      <TableCell align="center" colSpan={6} sx={{ py: 3 }}>
                        <SearchNotFound searchQuery={filterName} sx={{width: '100%'}} />
                      </TableCell>
                    </TableRow>
                  </TableBody>
                )}
              </Table>
            </TableContainer>
          <TablePagination
            rowsPerPageOptions={[5, 10, 25]}
            component="div"
            count={data.length}
            rowsPerPage={rowsPerPage}
            page={page}
            onPageChange={handleChangePage}
            onRowsPerPageChange={handleChangeRowsPerPage}
          />
        </Card>
        </Grow>
      </Container>
    </Page>
  );
}
