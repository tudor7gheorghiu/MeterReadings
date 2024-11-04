import logo from './logo.svg';
import './App.css';
import './MeterUploader'
import './AccountsUploader'
import AccountsUploader from './AccountsUploader';
import MeterUploader from './MeterUploader';

function App() {
  return (
    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
          </header>
          <AccountsUploader />
          <MeterUploader />
    </div>
  );
}

export default App;
