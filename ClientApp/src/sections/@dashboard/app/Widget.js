// @mui
import {
    Card,
    Grid,
    Grow,
    IconButton,
    Typography
} from '@mui/material';
// components
import Iconify from '../../../components/Iconify';
import ListItems from '../../../components/ListItems';
import {useState} from "react";
export default function Widget({ title, uri, total, items, icon, loading, addComponent }) {
    const [open, setOpen] = useState(false);
    const handleClose = () => 
        setOpen(false)
  return (
      <>  {(open, handleClose)=>addComponent(open, handleClose)}
      <Grow in={!loading}>
    <Card
      sx={{
          height: 375,
        py: 5,
        boxShadow: 'rgba(149, 157, 165, 0.2) 0px 8px 24px',
        textAlign: 'center',
        paddingTop: 0,
      }}>
      <div
          style={{
            display:'absolute',
            background: 'linear-gradient(60deg, #D9D7C3 ,#C7D9C9 )',
            height: 80,
            top: 0,
      }}
      >
        <Grid height={'100%'} alignItems={'center'} justifyContent={'space-between'} container>
          <Grid marginLeft={3} item>
              <Grid direction={'row'} gap={1.5} alignItems={'center'}  container>
                  <Grid item>
              <Iconify icon={icon} width={32} height={32} />
                  </Grid>
                  <Grid item>
              <Grid direction={'column'} alignItems={'flex-start'} container>
                  <Grid item>
                      <Typography variant="h6" fontWeight={'bold'} lineHeight={1.25}>
                          {title}    
                      </Typography>
                  </Grid>
                  <Grid item>
                      <Typography variant="h7" sx={{color:(theme) => theme.palette['primary'].lighter}}>
                          {total} {title.toLowerCase()}
                      </Typography>
                  </Grid>
                 </Grid>
                  </Grid>
              </Grid>
          </Grid>
          <Grid  marginRight={2.25} item>
              <IconButton onClick={addComponent ? ()=>setOpen(true) : undefined}>
              <Iconify icon={'dashicons:plus-alt2'} sx={{color:(theme) => theme.palette['primary'].lighter}}/>
              </IconButton>
          </Grid>
        </Grid>
      </div>
        <ListItems uri={uri} items={items}/>
    </Card>
      </Grow>
      </>
  );
}
