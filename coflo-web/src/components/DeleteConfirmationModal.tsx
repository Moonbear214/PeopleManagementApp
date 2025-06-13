import { Modal, Button } from 'react-bootstrap';

interface DeleteConfirmationModalProps {
    show: boolean;
    onHide: () => void;
    onConfirm: () => void;
    isDeleting: boolean;
    itemName?: string;
}

export function DeleteConfirmationModal({ show, onHide, onConfirm, isDeleting, itemName }: DeleteConfirmationModalProps) {
    return (
        <Modal show={show} onHide={onHide} centered>
            <Modal.Header closeButton>
                <Modal.Title>Confirm Deletion</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                Are you sure you want to delete {itemName ?? 'this item'}? This action cannot be undone.
            </Modal.Body>
            <Modal.Footer>
                <Button variant="secondary" onClick={onHide} disabled={isDeleting}>
                    Cancel
                </Button>
                <Button variant="danger" onClick={onConfirm} disabled={isDeleting}>
                    {isDeleting ? 'Deleting...' : 'Delete'}
                </Button>
            </Modal.Footer>
        </Modal>
    );
}