import config from '../config';
import type { Person, CreateUpdatePersonDto } from '../types';

const API_URL = `${config.apiBaseUrl}/Person`;

const handleResponse = async <T>(response: Response): Promise<T> => {
    if (!response.ok) {
        const errorText = await response.text();
        throw new Error(errorText || 'An API error occurred');
    }
    return response.status === 204 ? (null as T) : response.json();
};

// Retrieve a single person based on Id
export const getPersonById = (id: number): Promise<Person> => {
    return fetch(`${API_URL}/${id}`).then(res => handleResponse<Person>(res));
};

// Call to create a new person
export const createPerson = (person: CreateUpdatePersonDto): Promise<Person> => {
    return fetch(API_URL, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(person),
    }).then(res => handleResponse<Person>(res));
};

// Call to update a single person
export const updatePerson = (id: number, person: CreateUpdatePersonDto): Promise<void> => {
    return fetch(`${API_URL}/${id}`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(person),
    }).then(res => handleResponse<void>(res));
};

// Call to delete a single person based on Id
export const deletePerson = (id: number): Promise<void> => {
    return fetch(`${API_URL}/${id}`, {
        method: 'DELETE',
    }).then(res => handleResponse<void>(res));
};

// Retrieves list of all people
export const getAllPeople = (): Promise<Person[]> => {
    return fetch(`${API_URL}/getAllPeople`).then(res => handleResponse<Person[]>(res));
};

// Search for person by Name and Surname based on searchQuery
export const searchPeople = (searchQuery: string): Promise<Person[]> => {
    const url = new URL(`${API_URL}/search`);
    url.searchParams.append('searchQuery', searchQuery);
    return fetch(url.toString()).then(res => handleResponse<Person[]>(res));
};