import * as React from 'react';
import List from '@mui/material/List';
import Divider from '@mui/material/Divider';
import ListItemText from '@mui/material/ListItemText';
import Typography from '@mui/material/Typography';
import {Box, Card, Grow, IconButton, ListItemButton, ListSubheader, Slide, Stack} from "@mui/material";
import Iconify from "./Iconify";

export default function SimpleList({items, title, setSelect, setNestedSelect, path, selected, skinny, isDesktop}) {
    return (
        <Card sx={{height:450}}>
           
        <List  subheader={
            <Stack direction={'row'}>
                {!isDesktop && setNestedSelect &&
                    <IconButton onClick={() => {
                        setNestedSelect(null)
                    }}>
                        <Iconify icon="eva:arrow-back-outline" sx={{ color: 'text.disabled', width: 20, height: 20 }} />
                    </IconButton>
                }
                <ListSubheader component="div" id="nested-list-subheader">
                    {isDesktop ? title : 
                        path ? `${path}/${title}` : title}
                </ListSubheader>
            </Stack>
        }
                sx={{ width: '100%', minWidth: isDesktop && skinny ? 200 : 360, bgcolor: 'background.paper'}}>

            {items.length > 0 && items.map((item, index)=>
                <>
                    <ListItemButton key={`${item.id}-${title}-btn`} onClick={()=>setSelect(item)} alignItems="flex-start"
                    selected={selected && (item.id === selected.id)}>
                        <ListItemText key={`${item.id}-${title}-txt`}
                            primary={item.primary}
                            secondary={
                                <React.Fragment>
                                    <Typography key={`${item.id}-${title}-typ`}
                                        sx={{ display: 'inline' }}
                                        component="span"
                                        variant="body2"
                                        color="text.primary"
                                    >
                                        {item.secondary}
                                    </Typography>
                                    {item.tertiary}
                                </React.Fragment>
                            }
                        />
                    </ListItemButton>
                    {index !== items.length - 1 && <Divider key={`${item.id}-${title}-dvd`} component="li" />}
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