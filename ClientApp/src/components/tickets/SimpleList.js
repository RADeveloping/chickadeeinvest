import * as React from 'react';
import List from '@mui/material/List';
import Divider from '@mui/material/Divider';
import ListItemText from '@mui/material/ListItemText';
import Typography from '@mui/material/Typography';
import {
    Box,
    Card,
    Collapse,
    Grow,
    IconButton,
    ListItemButton,
    ListSubheader, Menu,
    MenuItem,
    Stack
} from "@mui/material";
import Iconify from "../common/Iconify";
import {useNavigate} from "react-router-dom";
import {useState} from "react";
import {abortFetch} from "../../utils/fetch";

/**
 * Generic list component used in ColumnOverview.
 * @param items {[object]} Array of items.
 * @param title {string} Title of list.
 * @param setSelectedId {function} Sets selectedId state.
 * @param setNestedSelect {function} Sets the nestedId state.
 * @param path {string} Path for mobile view.
 * @param selectedId {string} SelectedId state for this list of items.
 * @param skinny {boolean} Set the width of the column to skinny.
 * @param isDesktop {boolean} isDesktop state.
 * @param leftRound {boolean} Left border is rounded, for the left side of the column.
 * @param rightRound {boolean} Right border is rounded, for the right side of the column.
 * @param noRound {boolean} No rounded border, for the middle sections of the column view.
 * @param properties {[object]} Array of properties for sorting.
 * @param loading {boolean} Loading state.
 * @param disableSort {boolean} Hides sort button.
 * @param uri {string} URI string.
 * @param setOrderBy {function} Sets the orderBy state.
 * @param order {string} Order, desc or asc state.
 * @param setOrder {function} Sets the order state.
 * @param immediateClick {boolean} Immediate action after click, for use with the last column.
 * @param addComponent {()=>JSX.Element} Method that returns the add component, a plus symbol will appear.
 * @returns {JSX.Element}
 * @constructor
 */
export default function SimpleList({
                                       items,
                                       title,
                                       setSelectedId,
                                       setNestedSelect,
                                       path,
                                       selectedId,
                                       skinny,
                                       isDesktop,
                                       leftRound,
                                       rightRound,
                                       noRound,
                                       properties,
                                       loading,
                                       disableSort,
                                       uri,
                                       setOrderBy, order, setOrder,
                                       immediateClick,
                                       addComponent
                                   }) {
    const navigate = useNavigate();
    const [openAdd, setOpenAdd] = useState(false);
    const handleCloseAdd = () => setOpenAdd(false)
    const borderStyles = isDesktop ? {
        borderTopRightRadius: leftRound ? 0 : undefined, borderBottomRightRadius: leftRound ? 0 : undefined,
        borderTopLeftRadius: rightRound ? 0 : undefined, borderBottomLeftRadius: rightRound ? 0 : undefined,
        borderRadius: noRound ? 0 : undefined
    } : null

    const [anchorEl, setAnchorEl] = React.useState(null);
    const [selectedIndex, setSelectedIndex] = React.useState(0);
    const open = Boolean(anchorEl);

    const handleClickListItem = (event) => {
        setAnchorEl(event.currentTarget);
    };

    const handleMenuItemClick = (event, index) => {
        if (selectedIndex === index) {
            setOrder(order === 'desc' ? 'asc' : 'desc')
        }
        setOrderBy(properties[index].id)
        setSelectedIndex(index);
        setAnchorEl(null);
    };

    const handleClose = () => {
        setAnchorEl(null);
    };


    return (<>{addComponent && addComponent(openAdd, handleCloseAdd)}
            <Card sx={{height: '60vh', width: skinny && isDesktop ? '60%' : '100%', ...borderStyles}}>
                <List subheader={
                    <ListSubheader component="div" id="nested-list-subheader">
                        <Stack direction={'column'}>
                            <Collapse orientation="vertical" in={!isDesktop && setNestedSelect != null}>
                                <Box>
                                    <IconButton onClick={() => {
                                        setNestedSelect(null)
                                    }}>
                                        <Iconify icon="eva:arrow-back-outline"
                                                 sx={{color: 'text.disabled', width: 20, height: 20}}/>
                                    </IconButton>
                                    {path && `${path}`}
                                </Box>
                            </Collapse>
                            <Stack direction={'row'} justifyContent={'space-between'}>
                                {title}
                                <Stack direction={'row'} gap={1}>
                                    {!disableSort &&
                                        <>
                                            <Grow in={items.length > 0}>
                                                <div>
                                                    <IconButton onClick={handleClickListItem} size={'small'}>
                                                        <Iconify
                                                            icon={order === 'desc' ? 'cil:sort-descending' : 'cil:sort-ascending'}
                                                            sx={{color: (theme) => theme.palette['primary'].lighter}}/>
                                                    </IconButton>
                                                </div>
                                            </Grow>
                                            <Menu
                                                id="lock-menu"
                                                anchorEl={anchorEl}
                                                open={open}
                                                onClose={handleClose}
                                                MenuListProps={{
                                                    'aria-labelledby': 'lock-button',
                                                    role: 'listbox',
                                                }}
                                            >
                                                {properties.map((option, index) => (
                                                    <MenuItem
                                                        key={option.id}
                                                        selected={index === selectedIndex}
                                                        onClick={(event) => handleMenuItemClick(event, index)}
                                                    >
                                                        {option.label}
                                                    </MenuItem>
                                                ))}
                                            </Menu>
                                        </>
                                    }
                                    {addComponent &&
                                        <Grow in={true}>
                                            <div>
                                                <IconButton onClick={() => setOpenAdd(true)} size={'small'}>
                                                    <Iconify
                                                        icon={'dashicons:plus-alt2'}
                                                        sx={{color: (theme) => theme.palette['primary'].lighter}}/>
                                                </IconButton>
                                            </div>
                                        </Grow>
                                    }
                                </Stack>
                            </Stack>

                        </Stack>
                    </ListSubheader>
                }
                      sx={{
                          width: '100%',
                          minWidth: isDesktop && skinny ? 200 : 360,
                          bgcolor: 'background.paper',
                          overflowY: 'auto',
                          overflowX: 'clip',
                          height: '100%'
                      }}>

                    {items.length > 0 && items.map((item, index) =>
                        <React.Fragment key={'simpleList-item' + index}>
                            <Divider key={`${item.id}-${title}-dvd1`} component="li"/>
                            <ListItemButton key={`${item.id}-${title}-btn`} onClick={() => {
                                abortFetch();
                                if (immediateClick || (selectedId && selectedId === item.id)) {
                                    if (uri) {
                                        navigate(`/dashboard/${uri(item)}`);
                                    }
                                } else {
                                    setSelectedId(item.id)
                                }

                            }} alignItems="flex-start"
                                            selected={selectedId && (item.id === selectedId)}>
                                <ListItemText key={`${item.id}-${title}-txt`}
                                              primary={item.primary}
                                              secondary={
                                                  <>
                                                      <Typography key={`${item.id}-${title}-typ`}
                                                                  sx={{display: 'inline'}}
                                                                  component="span"
                                                                  variant="body2"
                                                                  color="text.primary"
                                                      >
                                                          {item.secondary}
                                                      </Typography>
                                                      {item.tertiary}
                                                  </>
                                              }
                                />
                            </ListItemButton>
                            {index === items.length - 1 &&
                                <Divider key={`${item.id}-${title}-dvd`} component="li"/>}
                        </React.Fragment>)
                    }
                    {items.length === 0 &&
                        <Box sx={{
                            height: isDesktop ? '75%' : '65%',
                            display: 'flex',
                            alignItems: 'center',
                            justifyContent: 'center',
                            color: 'gainsboro'
                        }}>{`No ${title}`}</Box>}
                </List>
            </Card>
        </>
    );
}