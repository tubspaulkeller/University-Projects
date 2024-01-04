
### Group Types
- Main
- Normel
- Private (2 users)

### Getting GroupData & Messages
- if in '/' path, get id of main channel
  - render MainRoom -> Child: Room (id)  
- in /users/:name/:id get private channel id
  -  render PrivateRoom -> Child Room (id)
- in /groups/:id we already have id
  - render NormalRooom -> Child Room (id)

- in Room
  - Get Room Users
  - Render Chat 
    - Chat get all Messages for Group (via. id)




