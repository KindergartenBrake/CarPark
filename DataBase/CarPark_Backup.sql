--
-- PostgreSQL database dump
--

\restrict nKfWpHOlPcidDvmiAkQc8rGN1Tm4eAUxK975Aay6xVlHkNvbE3W6b1xGMhJUPrk

-- Dumped from database version 18.3
-- Dumped by pg_dump version 18.3

-- Started on 2026-04-20 13:57:21

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 5 (class 2615 OID 17872)
-- Name: public; Type: SCHEMA; Schema: -; Owner: postgres
--

CREATE SCHEMA public;


ALTER SCHEMA public OWNER TO postgres;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 222 (class 1259 OID 18369)
-- Name: drivers; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.drivers (
    driver_id integer NOT NULL,
    user_id integer,
    vehicle_id integer,
    first_name character varying(50) NOT NULL,
    last_name character varying(50) NOT NULL,
    middle_name character varying(50),
    license_number character varying(20) NOT NULL,
    license_issue_date date NOT NULL,
    license_expiry_date date NOT NULL,
    birth_date date NOT NULL,
    phone character varying(20),
    email character varying(100),
    is_active boolean DEFAULT true
);


ALTER TABLE public.drivers OWNER TO postgres;

--
-- TOC entry 221 (class 1259 OID 18368)
-- Name: drivers_driver_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.drivers_driver_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.drivers_driver_id_seq OWNER TO postgres;

--
-- TOC entry 5120 (class 0 OID 0)
-- Dependencies: 221
-- Name: drivers_driver_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.drivers_driver_id_seq OWNED BY public.drivers.driver_id;


--
-- TOC entry 226 (class 1259 OID 18423)
-- Name: trip_requests; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.trip_requests (
    request_id integer NOT NULL,
    user_id integer NOT NULL,
    driver_id integer NOT NULL,
    vehicle_id integer NOT NULL,
    created_at timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
    description text,
    status character varying(20) DEFAULT 'pending'::character varying,
    CONSTRAINT trip_requests_status_check CHECK (((status)::text = ANY ((ARRAY['pending'::character varying, 'approved'::character varying, 'rejected'::character varying])::text[])))
);


ALTER TABLE public.trip_requests OWNER TO postgres;

--
-- TOC entry 225 (class 1259 OID 18422)
-- Name: trip_requests_request_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.trip_requests_request_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.trip_requests_request_id_seq OWNER TO postgres;

--
-- TOC entry 5121 (class 0 OID 0)
-- Dependencies: 225
-- Name: trip_requests_request_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.trip_requests_request_id_seq OWNED BY public.trip_requests.request_id;


--
-- TOC entry 228 (class 1259 OID 18454)
-- Name: trips; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.trips (
    trip_id integer NOT NULL,
    request_id integer NOT NULL,
    vehicle_id integer NOT NULL,
    trip_date date NOT NULL,
    start_time timestamp without time zone NOT NULL,
    end_time timestamp without time zone,
    start_odometer numeric(12,1) NOT NULL,
    end_odometer numeric(12,1),
    status character varying(20) DEFAULT 'planned'::character varying,
    CONSTRAINT trips_status_check CHECK (((status)::text = ANY ((ARRAY['planned'::character varying, 'active'::character varying, 'completed'::character varying, 'cancelled'::character varying])::text[])))
);


ALTER TABLE public.trips OWNER TO postgres;

--
-- TOC entry 227 (class 1259 OID 18453)
-- Name: trips_trip_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.trips_trip_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.trips_trip_id_seq OWNER TO postgres;

--
-- TOC entry 5122 (class 0 OID 0)
-- Dependencies: 227
-- Name: trips_trip_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.trips_trip_id_seq OWNED BY public.trips.trip_id;


--
-- TOC entry 220 (class 1259 OID 18351)
-- Name: users; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.users (
    user_id integer NOT NULL,
    username character varying(50) NOT NULL,
    password_hash character varying(255) NOT NULL,
    email character varying(100) NOT NULL,
    is_active boolean DEFAULT true,
    role character varying(20) NOT NULL,
    CONSTRAINT users_role_check CHECK (((role)::text = ANY ((ARRAY['admin'::character varying, 'driver'::character varying, 'employee'::character varying])::text[])))
);


ALTER TABLE public.users OWNER TO postgres;

--
-- TOC entry 219 (class 1259 OID 18350)
-- Name: users_user_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.users_user_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.users_user_id_seq OWNER TO postgres;

--
-- TOC entry 5123 (class 0 OID 0)
-- Dependencies: 219
-- Name: users_user_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.users_user_id_seq OWNED BY public.users.user_id;


--
-- TOC entry 230 (class 1259 OID 18492)
-- Name: v_drivers; Type: VIEW; Schema: public; Owner: postgres
--

CREATE VIEW public.v_drivers AS
 SELECT driver_id AS "Идентификатор_водителя",
    first_name AS "Имя",
    last_name AS "Фамилия",
    middle_name AS "Отчество",
    license_number AS "Номер_прав",
    license_issue_date AS "Дата_выдачи_прав",
    license_expiry_date AS "Дата_окончания_прав",
    birth_date AS "Дата_рождения",
    phone AS "Телефон",
    email AS "Email",
    is_active AS "Состояние"
   FROM public.drivers
  ORDER BY driver_id;


ALTER VIEW public.v_drivers OWNER TO postgres;

--
-- TOC entry 232 (class 1259 OID 18500)
-- Name: v_trip_requests; Type: VIEW; Schema: public; Owner: postgres
--

CREATE VIEW public.v_trip_requests AS
 SELECT request_id AS "Идентификатор_заявки",
    user_id AS "Идентификатор_пользователя",
    driver_id AS "Идентификатор_водителя",
    vehicle_id AS "Идентификатор_ТС",
    created_at AS "Дата_создания",
    description AS "Описание",
    status AS "Статус"
   FROM public.trip_requests
  ORDER BY request_id;


ALTER VIEW public.v_trip_requests OWNER TO postgres;

--
-- TOC entry 233 (class 1259 OID 18504)
-- Name: v_trips; Type: VIEW; Schema: public; Owner: postgres
--

CREATE VIEW public.v_trips AS
 SELECT trip_id AS "Идентификатор_поездки",
    request_id AS "Идентификатор_заявки",
    vehicle_id AS "Идентификатор_ТС",
    trip_date AS "Дата_поездки",
    start_time AS "Время_начала",
    end_time AS "Время_окончания",
    start_odometer AS "Пробег_в_начале",
    end_odometer AS "Пробег_в_конце",
    status AS "Статус"
   FROM public.trips
  ORDER BY trip_id;


ALTER VIEW public.v_trips OWNER TO postgres;

--
-- TOC entry 229 (class 1259 OID 18488)
-- Name: v_users; Type: VIEW; Schema: public; Owner: postgres
--

CREATE VIEW public.v_users AS
 SELECT user_id AS "Идентификатор пользователя",
    username AS "Имя_пользователя",
    password_hash AS "Хэш_пароля",
    email AS "Email",
    is_active AS "Состояние",
    role AS "Роль"
   FROM public.users
  ORDER BY user_id;


ALTER VIEW public.v_users OWNER TO postgres;

--
-- TOC entry 224 (class 1259 OID 18391)
-- Name: vehicles; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.vehicles (
    vehicle_id integer NOT NULL,
    driver_id integer,
    license_plate character varying(12) NOT NULL,
    vin character varying(17) NOT NULL,
    brand character varying(50) NOT NULL,
    model character varying(50) NOT NULL,
    year integer NOT NULL,
    mileage numeric(12,1) DEFAULT 0,
    fuel_type character varying(20),
    vehicle_type character varying(20),
    status character varying(20),
    insurance character varying(20),
    CONSTRAINT vehicles_fuel_type_check CHECK (((fuel_type)::text = ANY ((ARRAY['бензин'::character varying, 'дизель'::character varying, 'электро'::character varying, 'газ'::character varying])::text[]))),
    CONSTRAINT vehicles_insurance_check CHECK (((insurance)::text = ANY ((ARRAY['действует'::character varying, 'не действует'::character varying, 'оформляется'::character varying])::text[]))),
    CONSTRAINT vehicles_status_check CHECK (((status)::text = ANY ((ARRAY['в парке'::character varying, 'в ремонте'::character varying, 'на ТО'::character varying, 'списан'::character varying])::text[]))),
    CONSTRAINT vehicles_vehicle_type_check CHECK (((vehicle_type)::text = ANY ((ARRAY['легковой'::character varying, 'грузовой'::character varying, 'спецтехника'::character varying])::text[])))
);


ALTER TABLE public.vehicles OWNER TO postgres;

--
-- TOC entry 231 (class 1259 OID 18496)
-- Name: v_vehicles; Type: VIEW; Schema: public; Owner: postgres
--

CREATE VIEW public.v_vehicles AS
 SELECT vehicle_id AS "Идентификатор_ТС",
    driver_id AS "Идентификатор_водителя",
    license_plate AS "Госномер",
    vin AS "VIN_номер",
    brand AS "Марка",
    model AS "Модель",
    year AS "Год_выпуска",
    mileage AS "Пробег",
    fuel_type AS "Тип_топлива",
    vehicle_type AS "Тип_ТС",
    status AS "Состояние",
    insurance AS "Страховка"
   FROM public.vehicles
  ORDER BY vehicle_id;


ALTER VIEW public.v_vehicles OWNER TO postgres;

--
-- TOC entry 223 (class 1259 OID 18390)
-- Name: vehicles_vehicle_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.vehicles_vehicle_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.vehicles_vehicle_id_seq OWNER TO postgres;

--
-- TOC entry 5124 (class 0 OID 0)
-- Dependencies: 223
-- Name: vehicles_vehicle_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.vehicles_vehicle_id_seq OWNED BY public.vehicles.vehicle_id;


--
-- TOC entry 4898 (class 2604 OID 18372)
-- Name: drivers driver_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.drivers ALTER COLUMN driver_id SET DEFAULT nextval('public.drivers_driver_id_seq'::regclass);


--
-- TOC entry 4902 (class 2604 OID 18426)
-- Name: trip_requests request_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.trip_requests ALTER COLUMN request_id SET DEFAULT nextval('public.trip_requests_request_id_seq'::regclass);


--
-- TOC entry 4905 (class 2604 OID 18457)
-- Name: trips trip_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.trips ALTER COLUMN trip_id SET DEFAULT nextval('public.trips_trip_id_seq'::regclass);


--
-- TOC entry 4896 (class 2604 OID 18354)
-- Name: users user_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.users ALTER COLUMN user_id SET DEFAULT nextval('public.users_user_id_seq'::regclass);


--
-- TOC entry 4900 (class 2604 OID 18394)
-- Name: vehicles vehicle_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.vehicles ALTER COLUMN vehicle_id SET DEFAULT nextval('public.vehicles_vehicle_id_seq'::regclass);


--
-- TOC entry 5107 (class 0 OID 18369)
-- Dependencies: 222
-- Data for Name: drivers; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.drivers VALUES (1, 5, 1, 'Иван', 'Кузнецов', 'Петрович', '1234 567890', '2015-05-10', '2025-05-10', '1985-03-15', '+79161234567', 'kuznecov@carpark.com', true);
INSERT INTO public.drivers VALUES (2, 6, 2, 'Алексей', 'Смирнов', 'Иванович', '5678 123456', '2016-08-20', '2026-08-20', '1990-07-22', '+79167654321', 'smirnov@carpark.com', true);


--
-- TOC entry 5111 (class 0 OID 18423)
-- Dependencies: 226
-- Data for Name: trip_requests; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.trip_requests VALUES (1, 2, 1, 1, '2026-04-20 13:24:09.547376', 'Доставка документов в офис на ул. Ленина, 10', 'pending');
INSERT INTO public.trip_requests VALUES (2, 3, 2, 2, '2026-04-20 13:24:09.547376', 'Забрать оборудование со склада', 'approved');
INSERT INTO public.trip_requests VALUES (3, 4, 1, 3, '2026-04-20 13:24:09.547376', 'Встреча с партнёрами в аэропорту', 'rejected');


--
-- TOC entry 5113 (class 0 OID 18454)
-- Dependencies: 228
-- Data for Name: trips; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.trips VALUES (1, 2, 2, '2025-01-15', '2025-01-15 09:00:00', '2025-01-15 11:30:00', 35000.0, 35250.0, 'completed');
INSERT INTO public.trips VALUES (2, 1, 1, '2025-01-20', '2025-01-20 14:00:00', NULL, 15000.0, NULL, 'active');


--
-- TOC entry 5105 (class 0 OID 18351)
-- Dependencies: 220
-- Data for Name: users; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.users VALUES (1, 'admin', 'admin123', 'admin@carpark.com', true, 'admin');
INSERT INTO public.users VALUES (2, 'ivanov', 'pass123', 'ivanov@carpark.com', true, 'employee');
INSERT INTO public.users VALUES (3, 'petrov', 'pass123', 'petrov@carpark.com', true, 'employee');
INSERT INTO public.users VALUES (4, 'sidorov', 'pass123', 'sidorov@carpark.com', true, 'employee');
INSERT INTO public.users VALUES (5, 'driver_kuznecov', 'pass123', 'kuznecov@carpark.com', true, 'driver');
INSERT INTO public.users VALUES (6, 'driver_smirnov', 'pass123', 'smirnov@carpark.com', true, 'driver');


--
-- TOC entry 5109 (class 0 OID 18391)
-- Dependencies: 224
-- Data for Name: vehicles; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.vehicles VALUES (3, NULL, 'С789АВ777', 'XTA12345678901234', 'Kia', 'Rio', 2023, 5000.0, 'бензин', 'легковой', 'на ТО', 'действует');
INSERT INTO public.vehicles VALUES (4, NULL, 'Е101ММ777', 'ZFA12345678901234', 'Ford', 'Focus', 2020, 60000.0, 'дизель', 'легковой', 'в ремонте', 'не действует');
INSERT INTO public.vehicles VALUES (5, NULL, 'Т202ГР777', 'W0L12345678901234', 'GAZ', 'Газель', 2019, 120000.0, 'дизель', 'грузовой', 'в парке', 'действует');
INSERT INTO public.vehicles VALUES (6, NULL, 'С305РУ777', 'VIN12345678901234', 'MAN', 'TGS', 2021, 80000.0, 'дизель', 'грузовой', 'в парке', 'действует');
INSERT INTO public.vehicles VALUES (7, NULL, 'К408АМ777', 'ABC12345678901234', 'CAT', '320D', 2018, 5000.0, 'дизель', 'спецтехника', 'в ремонте', 'не действует');
INSERT INTO public.vehicles VALUES (1, 1, 'А123ВС777', 'WDB12345678901234', 'Toyota', 'Camry', 2022, 15250.0, 'бензин', 'легковой', 'в парке', 'действует');
INSERT INTO public.vehicles VALUES (2, 2, 'В456ОК777', 'JHM12345678901234', 'Hyundai', 'Solaris', 2021, 35250.0, 'бензин', 'легковой', 'в парке', 'действует');


--
-- TOC entry 5125 (class 0 OID 0)
-- Dependencies: 221
-- Name: drivers_driver_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.drivers_driver_id_seq', 2, true);


--
-- TOC entry 5126 (class 0 OID 0)
-- Dependencies: 225
-- Name: trip_requests_request_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.trip_requests_request_id_seq', 3, true);


--
-- TOC entry 5127 (class 0 OID 0)
-- Dependencies: 227
-- Name: trips_trip_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.trips_trip_id_seq', 2, true);


--
-- TOC entry 5128 (class 0 OID 0)
-- Dependencies: 219
-- Name: users_user_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.users_user_id_seq', 6, true);


--
-- TOC entry 5129 (class 0 OID 0)
-- Dependencies: 223
-- Name: vehicles_vehicle_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.vehicles_vehicle_id_seq', 7, true);


--
-- TOC entry 4921 (class 2606 OID 18384)
-- Name: drivers drivers_license_number_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.drivers
    ADD CONSTRAINT drivers_license_number_key UNIQUE (license_number);


--
-- TOC entry 4923 (class 2606 OID 18382)
-- Name: drivers drivers_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.drivers
    ADD CONSTRAINT drivers_pkey PRIMARY KEY (driver_id);


--
-- TOC entry 4937 (class 2606 OID 18437)
-- Name: trip_requests trip_requests_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.trip_requests
    ADD CONSTRAINT trip_requests_pkey PRIMARY KEY (request_id);


--
-- TOC entry 4941 (class 2606 OID 18467)
-- Name: trips trips_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.trips
    ADD CONSTRAINT trips_pkey PRIMARY KEY (trip_id);


--
-- TOC entry 4943 (class 2606 OID 18469)
-- Name: trips trips_request_id_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.trips
    ADD CONSTRAINT trips_request_id_key UNIQUE (request_id);


--
-- TOC entry 4915 (class 2606 OID 18367)
-- Name: users users_email_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_email_key UNIQUE (email);


--
-- TOC entry 4917 (class 2606 OID 18363)
-- Name: users users_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_pkey PRIMARY KEY (user_id);


--
-- TOC entry 4919 (class 2606 OID 18365)
-- Name: users users_username_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_username_key UNIQUE (username);


--
-- TOC entry 4928 (class 2606 OID 18409)
-- Name: vehicles vehicles_license_plate_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.vehicles
    ADD CONSTRAINT vehicles_license_plate_key UNIQUE (license_plate);


--
-- TOC entry 4930 (class 2606 OID 18407)
-- Name: vehicles vehicles_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.vehicles
    ADD CONSTRAINT vehicles_pkey PRIMARY KEY (vehicle_id);


--
-- TOC entry 4932 (class 2606 OID 18411)
-- Name: vehicles vehicles_vin_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.vehicles
    ADD CONSTRAINT vehicles_vin_key UNIQUE (vin);


--
-- TOC entry 4924 (class 1259 OID 18480)
-- Name: idx_drivers_user_id; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX idx_drivers_user_id ON public.drivers USING btree (user_id);


--
-- TOC entry 4925 (class 1259 OID 18481)
-- Name: idx_drivers_vehicle_id; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX idx_drivers_vehicle_id ON public.drivers USING btree (vehicle_id);


--
-- TOC entry 4933 (class 1259 OID 18484)
-- Name: idx_trip_requests_driver_id; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX idx_trip_requests_driver_id ON public.trip_requests USING btree (driver_id);


--
-- TOC entry 4934 (class 1259 OID 18483)
-- Name: idx_trip_requests_user_id; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX idx_trip_requests_user_id ON public.trip_requests USING btree (user_id);


--
-- TOC entry 4935 (class 1259 OID 18485)
-- Name: idx_trip_requests_vehicle_id; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX idx_trip_requests_vehicle_id ON public.trip_requests USING btree (vehicle_id);


--
-- TOC entry 4938 (class 1259 OID 18486)
-- Name: idx_trips_request_id; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX idx_trips_request_id ON public.trips USING btree (request_id);


--
-- TOC entry 4939 (class 1259 OID 18487)
-- Name: idx_trips_vehicle_id; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX idx_trips_vehicle_id ON public.trips USING btree (vehicle_id);


--
-- TOC entry 4926 (class 1259 OID 18482)
-- Name: idx_vehicles_driver_id; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX idx_vehicles_driver_id ON public.vehicles USING btree (driver_id);


--
-- TOC entry 4944 (class 2606 OID 18385)
-- Name: drivers drivers_user_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.drivers
    ADD CONSTRAINT drivers_user_id_fkey FOREIGN KEY (user_id) REFERENCES public.users(user_id) ON DELETE SET NULL;


--
-- TOC entry 4945 (class 2606 OID 18417)
-- Name: drivers fk_drivers_vehicle; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.drivers
    ADD CONSTRAINT fk_drivers_vehicle FOREIGN KEY (vehicle_id) REFERENCES public.vehicles(vehicle_id) ON DELETE SET NULL;


--
-- TOC entry 4947 (class 2606 OID 18443)
-- Name: trip_requests trip_requests_driver_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.trip_requests
    ADD CONSTRAINT trip_requests_driver_id_fkey FOREIGN KEY (driver_id) REFERENCES public.drivers(driver_id) ON DELETE CASCADE;


--
-- TOC entry 4948 (class 2606 OID 18438)
-- Name: trip_requests trip_requests_user_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.trip_requests
    ADD CONSTRAINT trip_requests_user_id_fkey FOREIGN KEY (user_id) REFERENCES public.users(user_id) ON DELETE CASCADE;


--
-- TOC entry 4949 (class 2606 OID 18448)
-- Name: trip_requests trip_requests_vehicle_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.trip_requests
    ADD CONSTRAINT trip_requests_vehicle_id_fkey FOREIGN KEY (vehicle_id) REFERENCES public.vehicles(vehicle_id) ON DELETE CASCADE;


--
-- TOC entry 4950 (class 2606 OID 18470)
-- Name: trips trips_request_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.trips
    ADD CONSTRAINT trips_request_id_fkey FOREIGN KEY (request_id) REFERENCES public.trip_requests(request_id) ON DELETE CASCADE;


--
-- TOC entry 4951 (class 2606 OID 18475)
-- Name: trips trips_vehicle_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.trips
    ADD CONSTRAINT trips_vehicle_id_fkey FOREIGN KEY (vehicle_id) REFERENCES public.vehicles(vehicle_id) ON DELETE CASCADE;


--
-- TOC entry 4946 (class 2606 OID 18412)
-- Name: vehicles vehicles_driver_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.vehicles
    ADD CONSTRAINT vehicles_driver_id_fkey FOREIGN KEY (driver_id) REFERENCES public.drivers(driver_id) ON DELETE SET NULL;


--
-- TOC entry 5119 (class 0 OID 0)
-- Dependencies: 5
-- Name: SCHEMA public; Type: ACL; Schema: -; Owner: postgres
--

REVOKE USAGE ON SCHEMA public FROM PUBLIC;


-- Completed on 2026-04-20 13:57:21

--
-- PostgreSQL database dump complete
--

\unrestrict nKfWpHOlPcidDvmiAkQc8rGN1Tm4eAUxK975Aay6xVlHkNvbE3W6b1xGMhJUPrk

