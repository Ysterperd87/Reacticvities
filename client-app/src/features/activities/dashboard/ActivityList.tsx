import { observer } from 'mobx-react-lite';
import { SyntheticEvent, useState } from 'react';
import { Segment, Item, Button, Label } from 'semantic-ui-react';
import { Activity } from '../../../app/layout/models/activity';
import { useStore } from '../../../app/stores/store';

export default observer(function ActivityList() {
  const { activityStore } = useStore();
  const { loading, deleteActivity, activities } = activityStore;

  const [target, setTarget] = useState('');

  function handleActivityDelete(e: SyntheticEvent<HTMLButtonElement>, id: string) {
    setTarget(e.currentTarget.name);
    deleteActivity(id);
  }

  return (
    <Segment>
      <Item.Group divided>
        {activities.map((activity) => (
          <Item key={activity.id}>
            <Item.Content>
              <Item.Header as="a">{activity.title}</Item.Header>
              <Item.Meta>{activity.date}</Item.Meta>
              <Item.Description>
                <div>{activity.description}</div>
                <div>
                  {activity.city}, {activity.venue}
                </div>
              </Item.Description>
              <Item.Extra>
                <Button floated="right" content="View" color="blue" onClick={() => activityStore.selectActivity(activity.id)} />
                <Button
                  name={activity.id}
                  floated="right"
                  content="Delete"
                  color="red"
                  onClick={(e) => handleActivityDelete(e, activity.id)}
                  loading={loading && target === activity.id}
                />
                <Label basic content={activity.category} />
              </Item.Extra>
            </Item.Content>
          </Item>
        ))}
      </Item.Group>
    </Segment>
  );
});
