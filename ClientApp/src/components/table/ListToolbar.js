import PropTypes from 'prop-types';
// material
import {styled} from '@mui/material/styles';
import {
    Toolbar,
    Tooltip,
    IconButton,
    Typography,
    OutlinedInput,
    InputAdornment,
    Popover,
    MenuItem, Select, Box, Fade, InputLabel, FormControl
} from '@mui/material';
// component
import * as React from "react";
import {useRef, useState} from "react";
import Iconify from "../common/Iconify";

// ----------------------------------------------------------------------

const RootStyle = styled(Toolbar)(({theme}) => ({
    height: 96,
    display: 'flex',
    justifyContent: 'space-between',
    padding: theme.spacing(0, 1, 0, 3),
}));

const SearchStyle = styled(OutlinedInput)(({theme}) => ({
    width: 150,
    transition: theme.transitions.create(['box-shadow', 'width'], {
        easing: theme.transitions.easing.easeInOut,
        duration: theme.transitions.duration.shorter,
    }),
    '&.Mui-focused': {boxShadow: theme.customShadows.z8},
    '&.extend': {width: 300},
    '& fieldset': {
        borderWidth: `1px !important`,
        borderColor: `${theme.palette.grey[500_32]} !important`,
    },
}));

// ----------------------------------------------------------------------

ListToolbar.propTypes = {
    numSelected: PropTypes.number,
    filterName: PropTypes.string,
    onFilterName: PropTypes.func,
};

export default function ListToolbar({
                                        numSelected,
                                        filterQuery,
                                        setFilterQuery,
                                        properties,
                                        isDesktop
                                    }) {
    const onFilterQuery = (event) => {
        setFilterQuery(event.target.value);
    };
    const [filterVisible, setFilterVisible] = useState(false);
    return (
        <RootStyle
            sx={{
                padding: 2.5,
                ...(numSelected > 0 && {
                    color: 'primary.main',
                    bgcolor: 'primary.lighter',
                }),
            }}
        >
            {numSelected > 0 ? (
                <Typography component="div" variant="subtitle1">
                    {numSelected} selected
                </Typography>
            ) : (
                <>
                    <Box width={'100%'}>
                        <SearchStyle
                            sx={{width: !isDesktop ? '100%' : undefined}}
                            className={filterVisible && isDesktop ? 'extend' : undefined}
                            value={filterQuery}
                            onChange={onFilterQuery}
                            onFocus={() => setFilterVisible(true)}
                            placeholder="Search"
                            startAdornment={
                                <InputAdornment position="start">
                                    <Iconify icon="eva:search-fill"
                                             sx={{color: 'text.disabled', width: 20, height: 20}}/>
                                </InputAdornment>
                            }

                            endAdornment={
                                <Fade in={filterVisible}>
                                    <IconButton onClick={() => {
                                        setFilterQuery('')
                                        setFilterVisible(false);
                                    }}>
                                        <Iconify icon="material-symbols:cancel"
                                                 sx={{color: 'text.disabled', width: 20, height: 20}}/>
                                    </IconButton>
                                </Fade>
                            }
                        />
                    </Box>
                </>
            )}
            {numSelected > 0 ? (
                <Tooltip title="Delete">
                    <IconButton>
                        <Iconify icon="eva:trash-2-fill"/>
                    </IconButton>
                </Tooltip>
            ) : null}
        </RootStyle>
    );
}


