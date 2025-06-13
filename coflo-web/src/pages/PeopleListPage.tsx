import React, { useEffect, useState, useCallback } from 'react';
import { Link } from 'react-router-dom';
import { getAllPeople, searchPeople, deletePerson } from '../services/peopleService';
import type { Person } from '../types/index';
import { DeleteConfirmationModal } from '../components/DeleteConfirmationModal';
import { Table, Button, Form, InputGroup, Spinner, Alert } from 'react-bootstrap';
import { PlusCircle, Search, Trash2, Edit } from 'lucide-react';

export function PeopleListPage() {
    const [people, setPeople] = useState<Person[]>([]);
    const [searchQuery, setSearchQuery] = useState('');
    const [personToDelete, setPersonToDelete] = useState<Person | null>(null);
    
    const [isLoading, setIsLoading] = useState(true);
    const [isDeleting, setIsDeleting] = useState(false);
    const [error, setError] = useState<string | null>(null);

    const fetchPeople = useCallback(() => {
        setIsLoading(true);
        setError(null);
        
        const promise = searchQuery
            ? searchPeople(searchQuery)
            : getAllPeople();

        promise
            .then(setPeople)
            .catch(err => setError(err.message))
            .finally(() => setIsLoading(false));
    }, [searchQuery]);

    useEffect(() => {
        fetchPeople();
    }, [fetchPeople]);

    const handleConfirmDelete = () => {
        if (personToDelete) {
            setIsDeleting(true);
            setError(null);
            deletePerson(personToDelete.id)
                .then(() => {
                    setPersonToDelete(null);

                    // Refresh List after Delete
                    fetchPeople();
                })
                .catch(err => setError(err.message))
                .finally(() => setIsDeleting(false));
        }
    };

    return (
        <div>
            <div className="d-flex justify-content-between align-items-center mb-4">
                <h1 className="h2">People</h1>
                <Link to="/add" className="btn btn-primary d-flex align-items-center">
                    <PlusCircle size={18} className="me-2" />
                    Add Person
                </Link>
            </div>

            <InputGroup className="mb-4">
                <InputGroup.Text><Search size={18} /></InputGroup.Text>
                <Form.Control 
                    placeholder="Search by first or last name..."
                    value={searchQuery}
                    onChange={(e: React.ChangeEvent<HTMLInputElement>) => setSearchQuery(e.target.value)}
                />
            </InputGroup>

            {error && <Alert variant="danger">{error}</Alert>}

            {isLoading ? (
                <div className="text-center p-5">
                    <Spinner animation="border" role="status">
                        <span className="visually-hidden">Loading...</span>
                    </Spinner>
                </div>
            ) : (
                <Table striped bordered hover responsive>
                    <thead>
                        <tr>
                            <th>Name</th>
                            <th>Age</th>
                            <th>Date of Birth</th>
                            <th>Actions</th>
                        </tr>
                    </thead>
                    <tbody>
                        {people.map(person => (
                            <tr key={person.id}>
                                <td>{person.firstName} {person.lastName}</td>
                                <td>{person.age}</td>
                                <td>{new Date(person.dateOfBirth).toLocaleDateString()}</td>
                                <td>
                                    <Link to={`/edit/${person.id}`} className="btn btn-sm btn-outline-secondary me-2">
                                        <Edit size={16} />
                                    </Link>
                                    <Button variant="outline-danger" size="sm" onClick={() => setPersonToDelete(person)}>
                                        <Trash2 size={16} />
                                    </Button>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </Table>
            )}
            
            <DeleteConfirmationModal
                show={personToDelete !== null}
                onHide={() => setPersonToDelete(null)}
                onConfirm={handleConfirmDelete}
                isDeleting={isDeleting}
                itemName={`${personToDelete?.firstName} ${personToDelete?.lastName}`}
            />
        </div>
    );
}