import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { Header, List } from 'semantic-ui-react';
import { Activity } from '../models/activity';
import NavBar from './NavBar';

function App() {

  const [activities, setActivities] = useState<Activity[]>([])

  useEffect(() => {
    
    axios.get<Activity[]>('http://localhost:5000/api/activities')
      .then(response => {
        //console.log(response.data)
        setActivities(response.data);
      })
      
  }, [])

  return (
    <div >
      <NavBar/>
      <Header as='h2' icon='users' content='Reactivities' />
      
      <List>
          {activities.map((activity) => (
            <List.Item key={activity.id}>
              {activity.title} - {activity.category}
            </List.Item>
          ))}
      </List>
    </div>
  );
}

export default App;
