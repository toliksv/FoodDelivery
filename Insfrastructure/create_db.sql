create database food_delivery

-- public.t_menu определение

-- Drop table

-- DROP TABLE public.t_menu;

CREATE TABLE public.t_menu (
	menu_item_id int4 GENERATED ALWAYS AS IDENTITY( INCREMENT BY 1 MINVALUE 1 MAXVALUE 2147483647 START 1 CACHE 1 NO CYCLE) NOT NULL,
	menu_item_name varchar(2000) NOT NULL,
	description text NULL,
	price numeric NOT NULL,
	CONSTRAINT t_menu_pk PRIMARY KEY (menu_item_id)
);


-- public.t_order_events определение

-- Drop table

-- DROP TABLE public.t_order_events;

CREATE TABLE public.t_order_events (
	order_event_id int8 GENERATED ALWAYS AS IDENTITY( INCREMENT BY 1 MINVALUE 1 MAXVALUE 9223372036854775807 START 1 CACHE 1 NO CYCLE) NOT NULL,
	order_id int4 NOT NULL,
	client_id int4 NOT NULL,
	order_event_type int4 NOT NULL,
	order_event_data jsonb NULL,
	occurred_on timestamp NOT NULL,
	CONSTRAINT t_order_events_pk PRIMARY KEY (order_event_id)
);
CREATE INDEX t_order_events_client_id_idx ON public.t_order_events USING btree (client_id);
CREATE INDEX t_order_events_order_id_idx ON public.t_order_events USING btree (order_id);