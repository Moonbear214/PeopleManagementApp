import { BrowserRouter as Router, Routes, Route, NavLink } from 'react-router-dom';
import { PeopleListPage } from './pages/PeopleListPage';
import { PersonFormPage } from './pages/PersonFormPage';

function App() {
  return (
    <Router>
        <nav className="navbar navbar-expand-lg navbar-dark bg-dark">
            <div className="container">
                <NavLink className="navbar-brand" to="/">Co-Flo People Manager</NavLink>
                <div className="collapse navbar-collapse">
                    <div className="navbar-nav">
                        <NavLink className="nav-link" to="/">People</NavLink>
                    </div>
                </div>
            </div>
        </nav>
        
        <main className="container mt-4">
          <Routes>
            <Route path="/" element={<PeopleListPage />} />
            <Route path="/add" element={<PersonFormPage />} />
            <Route path="/edit/:id" element={<PersonFormPage />} />
          </Routes>
        </main>
    </Router>
  );
}

export default App;