export interface Person {
    id: number;
    firstName: string;
    lastName: string;
    age: number;
    dateOfBirth: string;
    dateCreated: string;
}

export type CreateUpdatePersonDto = Omit<Person, 'id' | 'age' | 'dateCreated'>;
