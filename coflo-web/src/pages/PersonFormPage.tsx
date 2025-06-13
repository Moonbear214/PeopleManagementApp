import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { getPersonById, createPerson, updatePerson } from '../services/peopleService';
import type { CreateUpdatePersonDto } from '../types/index';
import { Form, Button, Row, Col, Card, Spinner, Alert } from 'react-bootstrap';
import DatePicker from 'react-datepicker';
import "react-datepicker/dist/react-datepicker.css";

const initialFormState: CreateUpdatePersonDto = {
    firstName: '',
    lastName: '',
    dateOfBirth: new Date().toISOString(),
};

export function PersonFormPage() {
    const { id } = useParams<{ id: string }>();
    const navigate = useNavigate();
    const isEditing = Boolean(id);

    const [formData, setFormData] = useState<CreateUpdatePersonDto>(initialFormState);
    const [age, setAge] = useState<number | null>(null);

    const [isLoading, setIsLoading] = useState(false);
    const [isSaving, setIsSaving] = useState(false);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        if (isEditing && id) {
            setIsLoading(true);
            setError(null);
            getPersonById(Number(id))
                .then(person => setFormData(person))
                .catch(err => setError(err.message))
                .finally(() => setIsLoading(false));
        }
    }, [id, isEditing]);

    useEffect(() => {
        const today = new Date();
        const birthDate = new Date(formData.dateOfBirth);
        let calculatedAge = today.getFullYear() - birthDate.getFullYear();
        const m = today.getMonth() - birthDate.getMonth();
        if (m < 0 || (m === 0 && today.getDate() < birthDate.getDate())) {
            calculatedAge--;
        }
        setAge(calculatedAge >= 0 ? calculatedAge : null);
    }, [formData.dateOfBirth]);
    
    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setFormData(prev => ({ ...prev, [e.target.name]: e.target.value }));
    };

    const handleDateChange = (date: Date | null) => {
        if (date) {
            setFormData(prev => ({ ...prev, dateOfBirth: date.toISOString() }));
        }
    };

    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();
        setIsSaving(true);
        setError(null);

        const promise = isEditing && id
            ? updatePerson(Number(id), formData)
            : createPerson(formData);
        
        promise
            .then(() => navigate('/'))
            .catch(err => setError(err.message))
            .finally(() => setIsSaving(false));
    };

    if (isLoading) {
        return <div className="text-center p-5"><Spinner animation="border" /></div>;
    }

    return (
        <Card>
            <Card.Header as="h2">{isEditing ? 'Edit Person' : 'Add New Person'}</Card.Header>
            <Card.Body>
                <Form onSubmit={handleSubmit}>
                    {error && <Alert variant="danger">{error}</Alert>}
                    <Row className="mb-3">
                        <Form.Group as={Col} controlId="formFirstName">
                            <Form.Label>First Name</Form.Label>
                            <Form.Control type="text" name="firstName" value={formData.firstName} onChange={handleChange} required />
                        </Form.Group>
                        <Form.Group as={Col} controlId="formLastName">
                            <Form.Label>Last Name</Form.Label>
                            <Form.Control type="text" name="lastName" value={formData.lastName} onChange={handleChange} required />
                        </Form.Group>
                    </Row>
                    
                    <Row className="mb-3">
                       <Form.Group as={Col} controlId="formDateOfBirth">
                            <Form.Label>Date of Birth</Form.Label>
                            <DatePicker
                                selected={new Date(formData.dateOfBirth)}
                                onChange={handleDateChange}
                                className="form-control"
                                dateFormat="MMMM d, yyyy"
                                showYearDropdown
                                dropdownMode="select"
                            />
                        </Form.Group>
                        <Form.Group as={Col} controlId="formAge">
                            <Form.Label>Age</Form.Label>
                            <Form.Control type="text" value={age ?? ''} readOnly disabled />
                        </Form.Group>
                    </Row>

                    <Button variant="primary" type="submit" disabled={isSaving}>
                        {isSaving ? 'Saving...' : 'Save Changes'}
                    </Button>
                    <Button variant="secondary" className="ms-2" onClick={() => navigate('/')} disabled={isSaving}>
                        Cancel
                    </Button>
                </Form>
            </Card.Body>
        </Card>
    );
}
