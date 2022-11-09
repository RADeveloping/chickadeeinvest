// @mui
import {Avatar, Card, Grow, Typography} from '@mui/material';
import Loading from "../../../components/Loading";

export default function UserWidget({ account, unit, loading}) {
    const {profilePicture, firstName, unitNo, propertyName} = account;
  return (
      <Grow in={!loading}>
    <Card
      sx={{
        height: 375,
        py: 5,
          boxShadow: 'rgba(149, 157, 165, 0.2) 0px 8px 24px',
        textAlign: 'left',
        color: 'black',
        background: 'linear-gradient(60deg, #D9D7C3 ,#C7D9C9 )',
      }}
    >
        <div style={{
            height: '100%',
            display: 'flex',
            marginLeft: '10%',
            width: '100%',
            flexDirection: 'column',
            justifyContent: 'center'}}>
        <Avatar sx={{height: 130, width: 130, fontSize: 50}} src={`data:image/jpeg;base64,${profilePicture}`} alt={firstName} />
            <br/>
            <br/>
        <Typography variant="h3" sx={{lineHeight: 1.25}}>
            {firstName}
        </Typography>
        <Typography variant="h5" sx={{color:(theme) => theme.palette['primary'].lighter, fontWeight:'normal', lineHeight: 1.25}}>
            {propertyName}
        </Typography>
            <br/>
        <Typography variant="h5" sx={{lineHeight: 1.25}}>
            {unitNo && <>Unit #{unitNo}</>}
        </Typography>

        </div>
  
    </Card>
      </Grow>
  );
}
