CREATE TABLE Processes (
    ProcessID INT PRIMARY KEY,
    ProcessName VARCHAR(255),
    Description TEXT,
    DateCreated DATE
);

CREATE TABLE ProcessState (
    StateID INT PRIMARY KEY,
    ProcessID INT,
    StateName VARCHAR(255),
    Description TEXT,
    DateCreated DATE,
    CONSTRAINT fk_ProcessID FOREIGN KEY (ProcessID) REFERENCES Processes(ProcessID)
);

CREATE TABLE StateTransitions (
    TransitionID INT PRIMARY KEY,
    ProcessID INT,
    FromStateID INT,
    ToStateID INT,
    TransitionName VARCHAR(255),
    Description TEXT,
    DateCreated DATE,
    CONSTRAINT fk_ProcessID FOREIGN KEY (ProcessID) REFERENCES Processes(ProcessID),
    CONSTRAINT fk_FromStateID FOREIGN KEY (FromStateID) REFERENCES ProcessState(StateID),
    CONSTRAINT fk_ToStateID FOREIGN KEY (ToStateID) REFERENCES ProcessState(StateID)
);
