import * as React from 'react';
import List from '@mui/material/List';
import Divider from '@mui/material/Divider';
import ListItemText from '@mui/material/ListItemText';
import Typography from '@mui/material/Typography';
import {Box, Card, Collapse, Grow, IconButton, ListItemButton, ListSubheader, Slide, Stack} from "@mui/material";
import Iconify from "./Iconify";
import {useNavigate} from "react-router-dom";

export default function SimpleList({items, title, setSelectedId, setNestedSelect, path, selectedId, skinny, isDesktop, leftRound, rightRound, noRound}) {
    const navigate = useNavigate();
    const borderStyles = isDesktop ? {
        borderTopRightRadius: leftRound ? 0 : undefined, borderBottomRightRadius: leftRound ? 0 : undefined,
        borderTopLeftRadius: rightRound ? 0 : undefined, borderBottomLeftRadius: rightRound ? 0 : undefined,
        borderRadius: noRound ? 0 : undefined} : null
    
    return (
        <Card sx={{height:450, width: skinny && isDesktop ? '60%' : '100%', ...borderStyles}}>
           
        <List  subheader={
                <ListSubheader component="div" id="nested-list-subheader">
                    <Stack direction={'column'}>
                        <Collapse orientation="vertical" in={!isDesktop && setNestedSelect }>
                        <Box>
                        <IconButton onClick={() => {
                            setNestedSelect(null)
                        }}>
                            <Iconify icon="eva:arrow-back-outline" sx={{ color: 'text.disabled', width: 20, height: 20 }} />
                        </IconButton>
                    {path && `${path}`}
                        </Box>
                        </Collapse>
                        {title}
                    </Stack>
                </ListSubheader>
        }
                sx={{ width: '100%', minWidth: isDesktop && skinny ? 200 : 360, bgcolor: 'background.paper'}}>

            {items.length > 0 && items.map((item, index)=>
                <>
                    <Divider key={`${item.id}-${title}-dvd1`} component="li" />
                    <ListItemButton key={`${item.id}-${title}-btn`} onClick={()=>{
                        if (selectedId && selectedId === item.id) {
                            navigate(`/dashboard/${title.toLowerCase()}/${item.id}`);
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
                                        sx={{ display: 'inline' }}
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
                    {index === items.length - 1 && <Divider key={`${item.id}-${title}-dvd`} component="li" />}
                </>)
            }
        </List>
            {items.length === 0 &&  
                <Box sx={{
                height: '80%',
                display: 'flex',
                alignItems: 'center',
                justifyContent: 'center',
                color: 'gainsboro'
            }}>{`No ${title}`}</Box>}
        </Card>
    );
}