import * as React from 'react';
import Paper from '@mui/material/Paper';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell, { tableCellClasses } from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TablePagination from '@mui/material/TablePagination';
import TableRow from '@mui/material/TableRow';
import { fToNow } from '../utils/formatTime';
import { styled } from '@mui/material/styles';
import { Chip, Toolbar } from '@mui/material';

export default function StickyHeadTable({ props }) {
  const [page, setPage] = React.useState(0);
  const [rowsPerPage, setRowsPerPage] = React.useState(10);
  const StyledTableCell = styled(TableCell)(({ theme }) => ({
    [`&.${tableCellClasses.head}`]: {
      backgroundColor: theme.palette.background.paper,
      color: theme.palette.text.primary
    },
    [`&.${tableCellClasses.body}`]: {
      fontSize: 14
    }
  }));

  const handleChangePage = (event, newPage) => {
    setPage(newPage);
  };

  const handleChangeRowsPerPage = (event) => {
    setRowsPerPage(+event.target.value);
    setPage(0);
  };

  const columns = [
    { id: 'problem', label: 'Title', minWidth: 100 },
    { id: 'description', label: 'Description', minWidth: 100 },
    {
      id: 'status',
      label: 'Status'
    },
    {
      id: 'severity',
      label: 'Severity'
    },
    {
      id: 'createdOn',
      label: 'Created On',
      minWidth: 170,
      align: 'right'
    }
  ];

  return (
    <Paper sx={{ width: '100%', overflow: 'hidden' }}>
      <Toolbar
        sx={{
          backgroundColor: (theme) => theme.palette.primary.main,
          color: (theme) => theme.palette.secondary.main,
          fontWeight: 'bold'
        }}
      >
        Unit Tickets
      </Toolbar>
      <TableContainer sx={{ maxHeight: 440 }}>
        <Table stickyHeader aria-label="sticky table">
          <TableHead>
            <TableRow>
              {columns.map((column) => (
                <StyledTableCell
                  key={column.id}
                  align={column.align}
                  style={{
                    minWidth: column.minWidth
                  }}
                >
                  {column.label}
                </StyledTableCell>
              ))}
            </TableRow>
          </TableHead>
          <TableBody>
            {props.slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage).map((ticket) => {
              return (
                <TableRow hover role="checkbox" tabIndex={-1} key={ticket.ticketId}>
                  {columns.map((column) => {
                    const value = ticket[column.id];
                    if (column.id === 'severity') {
                      return (
                        <TableCell key={column.id} align={column.align}>
                          {value === 0 ? (
                            <Chip label="Low" color="success" />
                          ) : value === 1 ? (
                            <Chip label="Medium" color="warning" />
                          ) : (
                            <Chip label="High" color="error" />
                          )}
                        </TableCell>
                      );
                    } else if (column.id === 'status') {
                      return (
                        <TableCell key={column.id} align={column.align}>
                          {value === 0 ? <Chip label="Open" color="info" /> : null}
                        </TableCell>
                      );
                    } else {
                      return (
                        <TableCell key={column.id} align={column.align}>
                          {column.id === 'createdOn' ? fToNow(value) : value}
                        </TableCell>
                      );
                    }
                  })}
                </TableRow>
              );
            })}
          </TableBody>
        </Table>
      </TableContainer>
      <TablePagination
        rowsPerPageOptions={[10, 25, 100]}
        component="div"
        count={props.length}
        rowsPerPage={rowsPerPage}
        page={page}
        onPageChange={handleChangePage}
        onRowsPerPageChange={handleChangeRowsPerPage}
      />
    </Paper>
  );
}
