Table users { 
  id uuid [primary key]
  fullname varchar
  email varchar
  password varchar
  created_at timestamp
  updated_at timestamp
}

Table articles {
  id integer [primary key, increment]
  user_id uuid [ref: > users.id]
  title varchar
  description text 
  photo_link varchar 
  created_at timestamp
  updated_at timestamp
}

Table bookings {
  user_id uuid [ref: > users.id]
  workspace_id integer [ref: > workspaces.id]
  status varchar
  booked_at day 
  duration_of_use int 
  payment_method varchar
  created_at timestamp
  updated_at timestamp
}

Table workspaces {
  id integer [primary key, increment]
  name varchar 
  description text
  location varchar
  type varchar
  price integer 
  created_at timestamp 
  updated_at timestamp
}