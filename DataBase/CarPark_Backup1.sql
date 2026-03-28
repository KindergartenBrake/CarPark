--
-- PostgreSQL database dump
--

\restrict KCG3K7c3d9ZMgmKaaqG3dQdSxIq1v0ECTcX3KaJKaCbIS7zTIgUlThEBbRXUsxu

-- Dumped from database version 18.3
-- Dumped by pg_dump version 18.3

-- Started on 2026-03-28 22:46:28

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
-- TOC entry 5103 (class 1262 OID 16388)
-- Name: CarPark; Type: DATABASE; Schema: -; Owner: postgres
--

CREATE DATABASE "CarPark" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'Russian_Russia.1251';


ALTER DATABASE "CarPark" OWNER TO postgres;

\unrestrict KCG3K7c3d9ZMgmKaaqG3dQdSxIq1v0ECTcX3KaJKaCbIS7zTIgUlThEBbRXUsxu
\connect "CarPark"
\restrict KCG3K7c3d9ZMgmKaaqG3dQdSxIq1v0ECTcX3KaJKaCbIS7zTIgUlThEBbRXUsxu

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
-- TOC entry 5104 (class 0 OID 0)
-- Dependencies: 5103
-- Name: DATABASE "CarPark"; Type: COMMENT; Schema: -; Owner: postgres
--

COMMENT ON DATABASE "CarPark" IS 'База данных автопарка компании';


--
-- TOC entry 5 (class 2615 OID 17872)
-- Name: public; Type: SCHEMA; Schema: -; Owner: postgres
--

CREATE SCHEMA public;


ALTER SCHEMA public OWNER TO postgres;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 219 (class 1259 OID 17873)
-- Name: drivers; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.drivers (
    driver_id integer NOT NULL,
    first_name character varying(50) NOT NULL,
    last_name character varying(50) NOT NULL,
    middle_name character varying(50) NOT NULL,
    license_number character varying(50) NOT NULL,
    license_issue_date date NOT NULL,
    license_expiry_date date NOT NULL,
    birth_date date NOT NULL,
    phone character varying(20) NOT NULL,
    email character varying(100) NOT NULL,
    hire_date date NOT NULL,
    is_active bit(1) NOT NULL
);


ALTER TABLE public.drivers OWNER TO postgres;

--
-- TOC entry 220 (class 1259 OID 17891)
-- Name: fuel_refuels; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.fuel_refuels (
    refuel_id integer NOT NULL,
    vehicle_id integer NOT NULL,
    driver_id integer NOT NULL,
    refuel_date timestamp without time zone NOT NULL,
    liters numeric(10,2) NOT NULL,
    cost_per_liter numeric(10,2) NOT NULL,
    total_cost numeric(12,2) NOT NULL,
    odometer numeric(12,1) NOT NULL,
    fuel_station character varying(200) NOT NULL
);


ALTER TABLE public.fuel_refuels OWNER TO postgres;

--
-- TOC entry 221 (class 1259 OID 17909)
-- Name: fuel_types; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.fuel_types (
    fuel_type_id integer NOT NULL,
    fuel_name character varying(30) NOT NULL,
    description character varying(2000) NOT NULL
);


ALTER TABLE public.fuel_types OWNER TO postgres;

--
-- TOC entry 222 (class 1259 OID 17920)
-- Name: insurance_policies; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.insurance_policies (
    policy_id integer NOT NULL,
    vehicle_id integer NOT NULL,
    policy_number character varying(50) NOT NULL,
    insurer character varying(100) NOT NULL,
    policy_type character varying(30) NOT NULL,
    start_date date NOT NULL,
    end_date date NOT NULL
);


ALTER TABLE public.insurance_policies OWNER TO postgres;

--
-- TOC entry 223 (class 1259 OID 17934)
-- Name: maintenance; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.maintenance (
    maintenance_id integer NOT NULL,
    vehicle_id integer NOT NULL,
    maint_type_id integer NOT NULL,
    scheduled_date date NOT NULL,
    actual_date date NOT NULL,
    odometer numeric(12,1) NOT NULL,
    cost numeric(12,1) NOT NULL,
    service_provider character varying(200) NOT NULL,
    invoice_number character varying(50) NOT NULL,
    description character varying(1000) NOT NULL
);


ALTER TABLE public.maintenance OWNER TO postgres;

--
-- TOC entry 224 (class 1259 OID 17954)
-- Name: maintenance_type; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.maintenance_type (
    maint_type_id integer NOT NULL,
    type_name character varying(50) NOT NULL,
    recommended_interval_km integer NOT NULL,
    description character varying(2000) NOT NULL
);


ALTER TABLE public.maintenance_type OWNER TO postgres;

--
-- TOC entry 225 (class 1259 OID 17966)
-- Name: trips; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.trips (
    trip_id integer NOT NULL,
    driver_id integer NOT NULL,
    trip_date date NOT NULL,
    start_time timestamp without time zone NOT NULL,
    end_time timestamp without time zone NOT NULL,
    start_odometer numeric(12,1) NOT NULL,
    end_odometer numeric(12,1) NOT NULL,
    purpose character varying(1000) NOT NULL,
    status character varying(20) NOT NULL,
    vehicle_id integer
);


ALTER TABLE public.trips OWNER TO postgres;

--
-- TOC entry 226 (class 1259 OID 17985)
-- Name: vehicle_statuses; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.vehicle_statuses (
    status_id integer NOT NULL,
    status_name character varying(30) NOT NULL,
    description character varying(2000) NOT NULL
);


ALTER TABLE public.vehicle_statuses OWNER TO postgres;

--
-- TOC entry 227 (class 1259 OID 17996)
-- Name: vehicle_types; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.vehicle_types (
    type_id integer NOT NULL,
    type_name character varying(50) NOT NULL,
    description character varying(2000) NOT NULL
);


ALTER TABLE public.vehicle_types OWNER TO postgres;

--
-- TOC entry 228 (class 1259 OID 18007)
-- Name: vehicles; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.vehicles (
    vehicle_id integer NOT NULL,
    license_plate character varying(12) NOT NULL,
    vin character varying(17) NOT NULL,
    brand character varying(50) NOT NULL,
    model character varying(50) NOT NULL,
    year integer NOT NULL,
    mileage numeric(12,1) NOT NULL,
    type_id integer NOT NULL,
    status_id integer NOT NULL,
    purchase_date date NOT NULL,
    purchase_cost numeric(12,2) NOT NULL,
    fuel_type_id integer
);


ALTER TABLE public.vehicles OWNER TO postgres;

--
-- TOC entry 5088 (class 0 OID 17873)
-- Dependencies: 219
-- Data for Name: drivers; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 5089 (class 0 OID 17891)
-- Dependencies: 220
-- Data for Name: fuel_refuels; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 5090 (class 0 OID 17909)
-- Dependencies: 221
-- Data for Name: fuel_types; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 5091 (class 0 OID 17920)
-- Dependencies: 222
-- Data for Name: insurance_policies; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 5092 (class 0 OID 17934)
-- Dependencies: 223
-- Data for Name: maintenance; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 5093 (class 0 OID 17954)
-- Dependencies: 224
-- Data for Name: maintenance_type; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 5094 (class 0 OID 17966)
-- Dependencies: 225
-- Data for Name: trips; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 5095 (class 0 OID 17985)
-- Dependencies: 226
-- Data for Name: vehicle_statuses; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 5096 (class 0 OID 17996)
-- Dependencies: 227
-- Data for Name: vehicle_types; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 5097 (class 0 OID 18007)
-- Dependencies: 228
-- Data for Name: vehicles; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 4892 (class 2606 OID 17890)
-- Name: drivers drivers_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.drivers
    ADD CONSTRAINT drivers_pkey PRIMARY KEY (driver_id);


--
-- TOC entry 4895 (class 2606 OID 17906)
-- Name: fuel_refuels fuel_refuels_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.fuel_refuels
    ADD CONSTRAINT fuel_refuels_pkey PRIMARY KEY (refuel_id);


--
-- TOC entry 4900 (class 2606 OID 17919)
-- Name: fuel_types fuel_types_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.fuel_types
    ADD CONSTRAINT fuel_types_pkey PRIMARY KEY (fuel_type_id);


--
-- TOC entry 4903 (class 2606 OID 17932)
-- Name: insurance_policies insurance_policies_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.insurance_policies
    ADD CONSTRAINT insurance_policies_pkey PRIMARY KEY (policy_id);


--
-- TOC entry 4907 (class 2606 OID 17951)
-- Name: maintenance maintenance_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.maintenance
    ADD CONSTRAINT maintenance_pkey PRIMARY KEY (maintenance_id);


--
-- TOC entry 4912 (class 2606 OID 17965)
-- Name: maintenance_type maintenance_type_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.maintenance_type
    ADD CONSTRAINT maintenance_type_pkey PRIMARY KEY (maint_type_id);


--
-- TOC entry 4915 (class 2606 OID 17982)
-- Name: trips trips_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.trips
    ADD CONSTRAINT trips_pkey PRIMARY KEY (trip_id);


--
-- TOC entry 4920 (class 2606 OID 17995)
-- Name: vehicle_statuses vehicle_statuses_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.vehicle_statuses
    ADD CONSTRAINT vehicle_statuses_pkey PRIMARY KEY (status_id);


--
-- TOC entry 4923 (class 2606 OID 18006)
-- Name: vehicle_types vehicle_types_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.vehicle_types
    ADD CONSTRAINT vehicle_types_pkey PRIMARY KEY (type_id);


--
-- TOC entry 4926 (class 2606 OID 18023)
-- Name: vehicles vehicles_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.vehicles
    ADD CONSTRAINT vehicles_pkey PRIMARY KEY (vehicle_id);


--
-- TOC entry 4896 (class 1259 OID 17907)
-- Name: xif1Заправки; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "xif1Заправки" ON public.fuel_refuels USING btree (vehicle_id);


--
-- TOC entry 4904 (class 1259 OID 17933)
-- Name: xif1Страховки; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "xif1Страховки" ON public.insurance_policies USING btree (vehicle_id);


--
-- TOC entry 4908 (class 1259 OID 17952)
-- Name: xif1Техобслуживание; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "xif1Техобслуживание" ON public.maintenance USING btree (maint_type_id);


--
-- TOC entry 4927 (class 1259 OID 18024)
-- Name: xif1Транспортные_средства; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "xif1Транспортные_средства" ON public.vehicles USING btree (type_id);


--
-- TOC entry 4897 (class 1259 OID 17908)
-- Name: xif2Заправки; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "xif2Заправки" ON public.fuel_refuels USING btree (driver_id);


--
-- TOC entry 4916 (class 1259 OID 17983)
-- Name: xif2Поездки; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "xif2Поездки" ON public.trips USING btree (driver_id);


--
-- TOC entry 4909 (class 1259 OID 17953)
-- Name: xif2Техобслуживание; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "xif2Техобслуживание" ON public.maintenance USING btree (vehicle_id);


--
-- TOC entry 4917 (class 1259 OID 17984)
-- Name: xif3Поездки; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "xif3Поездки" ON public.trips USING btree (vehicle_id);


--
-- TOC entry 4928 (class 1259 OID 18025)
-- Name: xif3Транспортные_средства; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "xif3Транспортные_средства" ON public.vehicles USING btree (status_id);


--
-- TOC entry 4929 (class 1259 OID 18026)
-- Name: xif4Транспортные_средства; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "xif4Транспортные_средства" ON public.vehicles USING btree (fuel_type_id);


--
-- TOC entry 4913 (class 1259 OID 17962)
-- Name: xpkВиды_ТО; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX "xpkВиды_ТО" ON public.maintenance_type USING btree (maint_type_id);


--
-- TOC entry 4893 (class 1259 OID 17887)
-- Name: xpkВодители; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX "xpkВодители" ON public.drivers USING btree (driver_id);


--
-- TOC entry 4898 (class 1259 OID 17902)
-- Name: xpkЗаправки; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX "xpkЗаправки" ON public.fuel_refuels USING btree (refuel_id);


--
-- TOC entry 4918 (class 1259 OID 17979)
-- Name: xpkПоездки; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX "xpkПоездки" ON public.trips USING btree (trip_id);


--
-- TOC entry 4921 (class 1259 OID 17992)
-- Name: xpkСтатусы_ТС; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX "xpkСтатусы_ТС" ON public.vehicle_statuses USING btree (status_id);


--
-- TOC entry 4905 (class 1259 OID 17929)
-- Name: xpkСтраховки; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX "xpkСтраховки" ON public.insurance_policies USING btree (policy_id);


--
-- TOC entry 4910 (class 1259 OID 17948)
-- Name: xpkТехобслуживание; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX "xpkТехобслуживание" ON public.maintenance USING btree (maintenance_id);


--
-- TOC entry 4924 (class 1259 OID 18003)
-- Name: xpkТипы_ТС; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX "xpkТипы_ТС" ON public.vehicle_types USING btree (type_id);


--
-- TOC entry 4901 (class 1259 OID 17916)
-- Name: xpkТипы_Топлива; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX "xpkТипы_Топлива" ON public.fuel_types USING btree (fuel_type_id);


--
-- TOC entry 4930 (class 1259 OID 18020)
-- Name: xpkТранспортные_средства; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX "xpkТранспортные_средства" ON public.vehicles USING btree (vehicle_id);


--
-- TOC entry 4931 (class 2606 OID 18032)
-- Name: fuel_refuels fuel_refuels_driver_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.fuel_refuels
    ADD CONSTRAINT fuel_refuels_driver_id_fkey FOREIGN KEY (driver_id) REFERENCES public.drivers(driver_id);


--
-- TOC entry 4932 (class 2606 OID 18027)
-- Name: fuel_refuels fuel_refuels_vehicle_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.fuel_refuels
    ADD CONSTRAINT fuel_refuels_vehicle_id_fkey FOREIGN KEY (vehicle_id) REFERENCES public.vehicles(vehicle_id);


--
-- TOC entry 4933 (class 2606 OID 18037)
-- Name: insurance_policies insurance_policies_vehicle_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.insurance_policies
    ADD CONSTRAINT insurance_policies_vehicle_id_fkey FOREIGN KEY (vehicle_id) REFERENCES public.vehicles(vehicle_id);


--
-- TOC entry 4934 (class 2606 OID 18042)
-- Name: maintenance maintenance_maint_type_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.maintenance
    ADD CONSTRAINT maintenance_maint_type_id_fkey FOREIGN KEY (maint_type_id) REFERENCES public.maintenance_type(maint_type_id);


--
-- TOC entry 4935 (class 2606 OID 18047)
-- Name: maintenance maintenance_vehicle_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.maintenance
    ADD CONSTRAINT maintenance_vehicle_id_fkey FOREIGN KEY (vehicle_id) REFERENCES public.vehicles(vehicle_id);


--
-- TOC entry 4936 (class 2606 OID 18052)
-- Name: trips trips_driver_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.trips
    ADD CONSTRAINT trips_driver_id_fkey FOREIGN KEY (driver_id) REFERENCES public.drivers(driver_id);


--
-- TOC entry 4937 (class 2606 OID 18057)
-- Name: trips trips_vehicle_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.trips
    ADD CONSTRAINT trips_vehicle_id_fkey FOREIGN KEY (vehicle_id) REFERENCES public.vehicles(vehicle_id);


--
-- TOC entry 4938 (class 2606 OID 18072)
-- Name: vehicles vehicles_fuel_type_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.vehicles
    ADD CONSTRAINT vehicles_fuel_type_id_fkey FOREIGN KEY (fuel_type_id) REFERENCES public.fuel_types(fuel_type_id);


--
-- TOC entry 4939 (class 2606 OID 18067)
-- Name: vehicles vehicles_status_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.vehicles
    ADD CONSTRAINT vehicles_status_id_fkey FOREIGN KEY (status_id) REFERENCES public.vehicle_statuses(status_id);


--
-- TOC entry 4940 (class 2606 OID 18062)
-- Name: vehicles vehicles_type_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.vehicles
    ADD CONSTRAINT vehicles_type_id_fkey FOREIGN KEY (type_id) REFERENCES public.vehicle_types(type_id);


--
-- TOC entry 5105 (class 0 OID 0)
-- Dependencies: 5
-- Name: SCHEMA public; Type: ACL; Schema: -; Owner: postgres
--

REVOKE USAGE ON SCHEMA public FROM PUBLIC;


-- Completed on 2026-03-28 22:46:28

--
-- PostgreSQL database dump complete
--

\unrestrict KCG3K7c3d9ZMgmKaaqG3dQdSxIq1v0ECTcX3KaJKaCbIS7zTIgUlThEBbRXUsxu

